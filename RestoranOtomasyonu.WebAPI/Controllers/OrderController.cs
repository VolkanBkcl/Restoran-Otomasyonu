using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RestoranOtomasyonu.Entities.Enums;
using RestoranOtomasyonu.WebAPI.Hubs;
using Microsoft.Data.SqlClient;
using System.Text;

namespace RestoranOtomasyonu.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IHubContext<SiparisHub> _hubContext;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IConfiguration configuration, IHubContext<SiparisHub> hubContext, ILogger<OrderController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true";
            _hubContext = hubContext;
            _logger = logger;
        }

        /// <summary>
        /// Yeni sipariş oluştur
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                _logger.LogInformation("Sipariş oluşturma isteği alındı. MasaId: {MasaId}, KullaniciId: {KullaniciId}, ItemCount: {ItemCount}", 
                    request?.MasaId, request?.KullaniciId, request?.Items?.Count);

                if (request == null)
                {
                    _logger.LogWarning("Request null geldi");
                    return BadRequest(new { message = "Request body boş olamaz.", success = false });
                }

                if (request.MasaId <= 0 || request.KullaniciId <= 0 || request.Items == null || request.Items.Count == 0)
                {
                    _logger.LogWarning("Geçersiz request parametreleri. MasaId: {MasaId}, KullaniciId: {KullaniciId}, Items: {Items}", 
                        request.MasaId, request.KullaniciId, request.Items?.Count);
                    return BadRequest(new { message = "Masa, kullanıcı ve sipariş kalemleri zorunludur.", success = false });
                }

                // Satış kodu oluştur
                var satisKodu = GenerateSatisKodu();
                decimal toplamTutar = 0;

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();

                    try
                    {
                        // Her bir sipariş kalemi için MasaHareketleri kaydı oluştur
                        foreach (var item in request.Items)
                        {
                            decimal birimFiyat = 0;

                            // Ürün veya Menü fiyatını al (BirimFiyati2 müşteri fiyatı)
                            if (item.UrunId > 0)
                            {
                                var fiyatQuery = "SELECT BirimFiyati2 FROM Urun WHERE Id = @Id";
                                using (var cmd = new SqlCommand(fiyatQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Id", item.UrunId);
                                    var result = cmd.ExecuteScalar();
                                    if (result != null && result != DBNull.Value)
                                        birimFiyat = Convert.ToDecimal(result);
                                }
                            }
                            // Not: Menu tablosu kategori tablosu, ürün fiyatı Urun tablosundan alınır

                            var tutar = birimFiyat * item.Miktari;
                            toplamTutar += tutar;

                            // MasaHareketleri kaydı
                            var insertQuery = @"INSERT INTO MasaHareketleri 
                                (SatisKodu, MasaId, MenuId, UrunId, Miktari, BirimMiktarı, BirimFiyati, Aciklama, Tarih)
                                VALUES 
                                (@SatisKodu, @MasaId, @MenuId, @UrunId, @Miktari, @BirimMiktari, @BirimFiyati, @Aciklama, @Tarih)";

                            using (var cmd = new SqlCommand(insertQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SatisKodu", satisKodu);
                                cmd.Parameters.AddWithValue("@MasaId", request.MasaId);
                                // MenuId ve UrunId kolonları NOT NULL - 0 gönder
                                cmd.Parameters.AddWithValue("@MenuId", item.MenuId > 0 ? item.MenuId : 0);
                                cmd.Parameters.AddWithValue("@UrunId", item.UrunId > 0 ? item.UrunId : 0);
                                cmd.Parameters.AddWithValue("@Miktari", item.Miktari);
                                cmd.Parameters.AddWithValue("@BirimMiktari", 1);
                                cmd.Parameters.AddWithValue("@BirimFiyati", birimFiyat);
                                cmd.Parameters.AddWithValue("@Aciklama", item.Aciklama ?? "");
                                cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Siparisler tablosuna kayıt
                        var indirimOrani = request.IndirimOrani ?? 0;
                        var indirimTutari = toplamTutar * (indirimOrani / 100);
                        var netTutar = toplamTutar - indirimTutari;

                        var siparisQuery = @"INSERT INTO Siparisler 
                            (MasaId, KullaniciId, SatisKodu, Tutar, IndirimOrani, NetTutar, OdemeDurumu, SiparisDurumu, Aciklama, Tarih)
                            VALUES 
                            (@MasaId, @KullaniciId, @SatisKodu, @Tutar, @IndirimOrani, @NetTutar, @OdemeDurumu, @SiparisDurumu, @Aciklama, @Tarih);
                            SELECT CAST(SCOPE_IDENTITY() as int);";

                        int siparisId;
                        using (var cmd = new SqlCommand(siparisQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MasaId", request.MasaId);
                            cmd.Parameters.AddWithValue("@KullaniciId", request.KullaniciId);
                            cmd.Parameters.AddWithValue("@SatisKodu", satisKodu);
                            cmd.Parameters.AddWithValue("@Tutar", toplamTutar);
                            cmd.Parameters.AddWithValue("@IndirimOrani", indirimOrani);
                            cmd.Parameters.AddWithValue("@NetTutar", netTutar);
                            // Yeni akış: ödeme bekliyor + onay bekliyor
                            cmd.Parameters.AddWithValue("@OdemeDurumu", (int)OdemeDurumu.OdemeBekliyor);
                            cmd.Parameters.AddWithValue("@SiparisDurumu", (int)SiparisDurumu.OnayBekliyor);
                            cmd.Parameters.AddWithValue("@Aciklama", request.Aciklama ?? "");
                            cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
                            siparisId = (int)cmd.ExecuteScalar();
                        }

                        transaction.Commit();

                        // SignalR ile masaüstü uygulamasına bildir
                        await _hubContext.Clients.All.SendAsync("ReceiveOrder", new
                        {
                            siparisId = siparisId,
                            masaId = request.MasaId,
                            kullaniciId = request.KullaniciId,
                            satisKodu = satisKodu,
                            toplamTutar = toplamTutar,
                            netTutar = netTutar,
                            tarih = DateTime.Now
                        });

                        return Ok(new
                        {
                            success = true,
                            message = "Sipariş başarıyla oluşturuldu.",
                            siparisId = siparisId,
                            satisKodu = satisKodu
                        });
                    }
                    catch (Exception innerEx)
                    {
                        transaction.Rollback();
                        _logger.LogError(innerEx, "Transaction içinde hata oluştu. MasaId: {MasaId}", request.MasaId);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş oluşturulurken beklenmeyen hata. MasaId: {MasaId}, KullaniciId: {KullaniciId}", 
                    request?.MasaId, request?.KullaniciId);
                return StatusCode(500, new { 
                    success = false,
                    message = "Sipariş oluşturulurken hata oluştu.", 
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        /// <summary>
        /// Masadaki tüm siparişleri getir
        /// </summary>
        [HttpGet("table/{masaId}")]
        public IActionResult GetTableOrders(int masaId)
        {
            try
            {
                var orders = new List<object>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = @"SELECT s.Id, s.MasaId, s.KullaniciId, s.SatisKodu, s.Tutar, s.IndirimOrani, s.NetTutar, 
                                         s.OdemeDurumu, s.SiparisDurumu, s.Aciklama, s.Tarih,
                                         k.AdSoyad, k.KullaniciAdi
                                 FROM Siparisler s
                                 INNER JOIN Kullanicilar k ON s.KullaniciId = k.Id
                                 WHERE s.MasaId = @MasaId
                                 ORDER BY s.Tarih DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MasaId", masaId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                orders.Add(new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    masaId = reader.GetInt32(reader.GetOrdinal("MasaId")),
                                    kullaniciId = reader.GetInt32(reader.GetOrdinal("KullaniciId")),
                                    kullaniciAdi = reader.IsDBNull(reader.GetOrdinal("KullaniciAdi")) ? "" : reader.GetString(reader.GetOrdinal("KullaniciAdi")),
                                    adSoyad = reader.IsDBNull(reader.GetOrdinal("AdSoyad")) ? "" : reader.GetString(reader.GetOrdinal("AdSoyad")),
                                    satisKodu = reader.IsDBNull(reader.GetOrdinal("SatisKodu")) ? "" : reader.GetString(reader.GetOrdinal("SatisKodu")),
                                    tutar = reader.GetDecimal(reader.GetOrdinal("Tutar")),
                                    indirimOrani = reader.GetDecimal(reader.GetOrdinal("IndirimOrani")),
                                    netTutar = reader.GetDecimal(reader.GetOrdinal("NetTutar")),
                                    odemeDurumu = reader.GetInt32(reader.GetOrdinal("OdemeDurumu")),
                                    siparisDurumu = reader.GetInt32(reader.GetOrdinal("SiparisDurumu")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    tarih = reader.GetDateTime(reader.GetOrdinal("Tarih"))
                                });
                            }
                        }
                    }
                }

                return Ok(new { success = true, data = orders });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Siparişler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Kullanıcının kendi siparişlerini getir (opsiyonel masaId filtresi)
        /// </summary>
        [HttpGet("my/{kullaniciId}")]
        public IActionResult GetMyOrders(int kullaniciId, [FromQuery] int? masaId = null)
        {
            try
            {
                var orders = new List<object>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = @"SELECT s.Id, s.MasaId, s.KullaniciId, s.SatisKodu, s.Tutar, s.IndirimOrani, s.NetTutar, 
                                         s.OdemeDurumu, s.SiparisDurumu, s.Aciklama, s.Tarih,
                                         m.MasaAdi
                                 FROM Siparisler s
                                 LEFT JOIN Masalar m ON s.MasaId = m.Id
                                 WHERE s.KullaniciId = @KullaniciId";
                    
                    if (masaId.HasValue && masaId.Value > 0)
                    {
                        query += " AND s.MasaId = @MasaId";
                    }
                    
                    query += " ORDER BY s.Tarih DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                        if (masaId.HasValue && masaId.Value > 0)
                        {
                            cmd.Parameters.AddWithValue("@MasaId", masaId.Value);
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                orders.Add(new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    masaId = reader.GetInt32(reader.GetOrdinal("MasaId")),
                                    masaAdi = reader.IsDBNull(reader.GetOrdinal("MasaAdi")) ? "" : reader.GetString(reader.GetOrdinal("MasaAdi")),
                                    satisKodu = reader.IsDBNull(reader.GetOrdinal("SatisKodu")) ? "" : reader.GetString(reader.GetOrdinal("SatisKodu")),
                                    tutar = reader.GetDecimal(reader.GetOrdinal("Tutar")),
                                    indirimOrani = reader.GetDecimal(reader.GetOrdinal("IndirimOrani")),
                                    netTutar = reader.GetDecimal(reader.GetOrdinal("NetTutar")),
                                    odemeDurumu = reader.GetInt32(reader.GetOrdinal("OdemeDurumu")),
                                    siparisDurumu = reader.GetInt32(reader.GetOrdinal("SiparisDurumu")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    tarih = reader.GetDateTime(reader.GetOrdinal("Tarih"))
                                });
                            }
                        }
                    }
                }

                return Ok(new { success = true, data = orders });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Siparişler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Masa özeti - Masadaki tüm siparişlerin özeti (SignalR için hazır)
        /// </summary>
        [HttpGet("table/{masaId}/summary")]
        public IActionResult GetTableSummary(int masaId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = @"SELECT 
                                    COUNT(DISTINCT s.KullaniciId) as KullaniciSayisi,
                                    COUNT(s.Id) as SiparisSayisi,
                                    SUM(CASE WHEN s.SiparisDurumu IN (0, 1, 2) THEN s.NetTutar ELSE 0 END) as ToplamTutar
                                 FROM Siparisler s
                                 WHERE s.MasaId = @MasaId";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MasaId", masaId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var summary = new
                                {
                                    kullaniciSayisi = reader.GetInt32(reader.GetOrdinal("KullaniciSayisi")),
                                    siparisSayisi = reader.GetInt32(reader.GetOrdinal("SiparisSayisi")),
                                    toplamTutar = reader.IsDBNull(reader.GetOrdinal("ToplamTutar")) ? 0 : reader.GetDecimal(reader.GetOrdinal("ToplamTutar"))
                                };

                                return Ok(new { success = true, data = summary });
                            }
                        }
                    }
                }

                return Ok(new { success = true, data = new { kullaniciSayisi = 0, siparisSayisi = 0, toplamTutar = 0 } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Masa özeti getirilirken hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Sipariş ödemesi (kendi payını öde)
        /// </summary>
        [HttpPost("pay/{siparisId}")]
        public async Task<IActionResult> PayOrder(int siparisId, [FromBody] PayOrderRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();

                    try
                    {
                        // Sipariş bilgilerini al
                        var getQuery = "SELECT KullaniciId, NetTutar, OdemeDurumu FROM Siparisler WHERE Id = @Id";
                        int kullaniciId = 0;
                        decimal netTutar = 0;
                        int odemeDurumu = 0;

                        using (var cmd = new SqlCommand(getQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", siparisId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    transaction.Rollback();
                                    return NotFound(new { message = "Sipariş bulunamadı." });
                                }
                                kullaniciId = reader.GetInt32(reader.GetOrdinal("KullaniciId"));
                                netTutar = reader.GetDecimal(reader.GetOrdinal("NetTutar"));
                                odemeDurumu = reader.GetInt32(reader.GetOrdinal("OdemeDurumu"));
                            }
                        }

                        // Yetki kontrolü - sadece kendi siparişini ödeyebilir
                        if (request.KullaniciId != kullaniciId)
                        {
                            transaction.Rollback();
                            return Forbid("Başkasının siparişini ödeyemezsiniz.");
                        }

                        // Ödeme durumunu güncelle
                        var updateQuery = "UPDATE Siparisler SET OdemeDurumu = @OdemeDurumu WHERE Id = @Id";
                        using (var cmd = new SqlCommand(updateQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", siparisId);
                            cmd.Parameters.AddWithValue("@OdemeDurumu", (int)OdemeDurumu.KendiOdedi);
                            cmd.ExecuteNonQuery();
                        }

                        // OdemeHareketleri tablosuna kayıt
                        var odemeQuery = @"INSERT INTO OdemeHareketleri 
                            (SatisKodu, OdemeTuru, Odenen, Aciklama, Tarih)
                            VALUES 
                            (@SatisKodu, @OdemeTuru, @Odenen, @Aciklama, @Tarih)";

                        using (var cmd = new SqlCommand(odemeQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@SatisKodu", request.SatisKodu ?? "");
                            cmd.Parameters.AddWithValue("@OdemeTuru", request.OdemeTuru ?? "Nakit");
                            cmd.Parameters.AddWithValue("@Odenen", netTutar);
                            cmd.Parameters.AddWithValue("@Aciklama", $"Kullanıcı {kullaniciId} - Kendi payı ödendi");
                            cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        // SignalR bildirimi
                        await _hubContext.Clients.All.SendAsync("OrderPaid", new
                        {
                            siparisId = siparisId,
                            kullaniciId = kullaniciId,
                            odenenTutar = netTutar
                        });

                        return Ok(new { success = true, message = "Ödeme başarıyla tamamlandı." });
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ödeme sırasında hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Sipariş iptal et (sadece kendi siparişini iptal edebilir)
        /// </summary>
        [HttpPost("cancel/{siparisId}")]
        public async Task<IActionResult> CancelOrder(int siparisId, [FromBody] CancelOrderRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();

                    try
                    {
                        // Sipariş bilgilerini al
                        var getQuery = "SELECT KullaniciId, SiparisDurumu, OdemeDurumu FROM Siparisler WHERE Id = @Id";
                        int kullaniciId = 0;
                        int siparisDurumu = 0;
                        int odemeDurumu = 0;

                        using (var cmd = new SqlCommand(getQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", siparisId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    transaction.Rollback();
                                    return NotFound(new { message = "Sipariş bulunamadı." });
                                }
                                kullaniciId = reader.GetInt32(reader.GetOrdinal("KullaniciId"));
                                siparisDurumu = reader.GetInt32(reader.GetOrdinal("SiparisDurumu"));
                                odemeDurumu = reader.GetInt32(reader.GetOrdinal("OdemeDurumu"));
                            }
                        }

                        // Yetki kontrolü - sadece kendi siparişini iptal edebilir
                        if (request.KullaniciId != kullaniciId)
                        {
                            transaction.Rollback();
                            return Forbid("Başkasının siparişini iptal edemezsiniz.");
                        }

                        // Ödeme kontrolü - ödenmiş sipariş iptal edilemez
                        if (odemeDurumu == 1 || odemeDurumu == 2)
                        {
                            transaction.Rollback();
                            return BadRequest(new { message = "Ödenmiş sipariş iptal edilemez." });
                        }

                        // Sipariş durumunu İptal Edildi (4) olarak güncelle
                        var updateQuery = "UPDATE Siparisler SET SiparisDurumu = 4 WHERE Id = @Id";
                        using (var cmd = new SqlCommand(updateQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", siparisId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        // SignalR bildirimi
                        await _hubContext.Clients.All.SendAsync("OrderCancelled", new
                        {
                            siparisId = siparisId,
                            masaId = 0 // MasaId'yi almak için ek sorgu gerekebilir
                        });

                        return Ok(new { success = true, message = "Sipariş iptal edildi." });
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş iptal edilirken hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Sipariş detayını getir (ödeme sayfası için)
        /// </summary>
        [HttpGet("detail/{siparisId}")]
        public IActionResult GetOrderDetail(int siparisId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"SELECT s.Id, s.MasaId, s.KullaniciId, s.SatisKodu, s.Tutar, s.IndirimOrani, s.NetTutar,
                                         s.OdemeDurumu, s.SiparisDurumu, s.Aciklama, s.Tarih,
                                         m.MasaAdi, k.KullaniciAdi, k.AdSoyad
                                  FROM Siparisler s
                                  LEFT JOIN Masalar m ON s.MasaId = m.Id
                                  LEFT JOIN Kullanicilar k ON s.KullaniciId = k.Id
                                  WHERE s.Id = @Id";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", siparisId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var detail = new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    masaId = reader.GetInt32(reader.GetOrdinal("MasaId")),
                                    masaAdi = reader.IsDBNull(reader.GetOrdinal("MasaAdi")) ? "" : reader.GetString(reader.GetOrdinal("MasaAdi")),
                                    kullaniciId = reader.GetInt32(reader.GetOrdinal("KullaniciId")),
                                    kullaniciAdi = reader.IsDBNull(reader.GetOrdinal("KullaniciAdi")) ? "" : reader.GetString(reader.GetOrdinal("KullaniciAdi")),
                                    adSoyad = reader.IsDBNull(reader.GetOrdinal("AdSoyad")) ? "" : reader.GetString(reader.GetOrdinal("AdSoyad")),
                                    satisKodu = reader.IsDBNull(reader.GetOrdinal("SatisKodu")) ? "" : reader.GetString(reader.GetOrdinal("SatisKodu")),
                                    tutar = reader.GetDecimal(reader.GetOrdinal("Tutar")),
                                    indirimOrani = reader.GetDecimal(reader.GetOrdinal("IndirimOrani")),
                                    netTutar = reader.GetDecimal(reader.GetOrdinal("NetTutar")),
                                    odemeDurumu = reader.GetInt32(reader.GetOrdinal("OdemeDurumu")),
                                    siparisDurumu = reader.GetInt32(reader.GetOrdinal("SiparisDurumu")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    tarih = reader.GetDateTime(reader.GetOrdinal("Tarih"))
                                };

                                return Ok(new { success = true, data = detail });
                            }
                        }
                    }
                }

                return NotFound(new { message = "Sipariş bulunamadı." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş detay alınırken hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Blockchain veya nakit ödeme tamamlandığında çağrılır
        /// </summary>
        [HttpPost("finalizePayment")]
        public async Task<IActionResult> FinalizePayment([FromBody] FinalizePaymentRequest request)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var transaction = connection.BeginTransaction();

                    try
                    {
                        // Sipariş var mı
                        var getQuery = "SELECT Id, MasaId, OdemeDurumu FROM Siparisler WHERE Id = @Id";
                        int masaId = 0;
                        int odemeDurumu = 0;

                        using (var cmd = new SqlCommand(getQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", request.SiparisId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    transaction.Rollback();
                                    return NotFound(new { message = "Sipariş bulunamadı." });
                                }
                                masaId = reader.GetInt32(reader.GetOrdinal("MasaId"));
                                odemeDurumu = reader.GetInt32(reader.GetOrdinal("OdemeDurumu"));
                            }
                        }

                        if (odemeDurumu == (int)OdemeDurumu.TumuOdendi)
                        {
                            transaction.Rollback();
                            return BadRequest(new { message = "Sipariş zaten ödenmiş." });
                        }

                        // Ödeme durumunu güncelle
                        var updateQuery = @"UPDATE Siparisler 
                                            SET OdemeDurumu = @OdemeDurumu, SiparisDurumu = @SiparisDurumu 
                                            WHERE Id = @Id";
                        using (var cmd = new SqlCommand(updateQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", request.SiparisId);
                            cmd.Parameters.AddWithValue("@OdemeDurumu", (int)OdemeDurumu.TumuOdendi);
                            cmd.Parameters.AddWithValue("@SiparisDurumu", (int)SiparisDurumu.OnayBekliyor);
                            cmd.ExecuteNonQuery();
                        }

                        // Opsiyonel: Tx kaydı eklemek isterseniz OdemeHareketleri tablosunu kullanabilirsiniz
                        if (!string.IsNullOrWhiteSpace(request.TxHash))
                        {
                            var odemeQuery = @"INSERT INTO OdemeHareketleri 
                                (SatisKodu, OdemeTuru, Odenen, Aciklama, Tarih)
                                VALUES 
                                (@SatisKodu, @OdemeTuru, @Odenen, @Aciklama, @Tarih)";

                            using (var cmd = new SqlCommand(odemeQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SatisKodu", request.SatisKodu ?? "");
                                cmd.Parameters.AddWithValue("@OdemeTuru", request.OdemeTuru ?? "Blockchain");
                                cmd.Parameters.AddWithValue("@Odenen", request.OdenenTutar ?? 0);
                                cmd.Parameters.AddWithValue("@Aciklama", $"Tx: {request.TxHash}");
                                cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        await _hubContext.Clients.All.SendAsync("OrderPaid", new
                        {
                            siparisId = request.SiparisId,
                            kullaniciId = 0,
                            odenenTutar = request.OdenenTutar ?? 0,
                            txHash = request.TxHash
                        });

                        return Ok(new { success = true, message = "Ödeme tamamlandı." });
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ödeme tamamlanırken hata oluştu.", error = ex.Message });
            }
        }

        private string GenerateSatisKodu()
        {
            // SatisKodu veritabanında varchar(15) - max 15 karakter olmalı
            // Format: yyyyMMddHHmmss (14 karakter) + random 1 karakter = 15 karakter
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(0, 9).ToString(); // 0-9 arası tek rakam
            return timestamp + random; // Toplam 15 karakter
        }
    }

    public class CreateOrderRequest
    {
        public int MasaId { get; set; }
        public int KullaniciId { get; set; }
        public decimal? IndirimOrani { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        public int UrunId { get; set; }
        public int MenuId { get; set; }
        public int Miktari { get; set; }
        public string Aciklama { get; set; } = string.Empty;
    }

    public class PayOrderRequest
    {
        public int KullaniciId { get; set; }
        public string SatisKodu { get; set; } = string.Empty;
        public string OdemeTuru { get; set; } = string.Empty;
    }

    public class CancelOrderRequest
    {
        public int KullaniciId { get; set; }
    }

    public class FinalizePaymentRequest
    {
        public int SiparisId { get; set; }
        public string? TxHash { get; set; }
        public string? OdemeTuru { get; set; } = "Blockchain";
        public decimal? OdenenTutar { get; set; }
        public string? SatisKodu { get; set; } = string.Empty;
    }
}

