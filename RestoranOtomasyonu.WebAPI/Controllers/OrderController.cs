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

        public OrderController(IConfiguration configuration, IHubContext<SiparisHub> hubContext)
        {
            _connectionString = "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true";
            _hubContext = hubContext;
        }

        /// <summary>
        /// Yeni sipariş oluştur
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                if (request.MasaId <= 0 || request.KullaniciId <= 0 || request.Items == null || request.Items.Count == 0)
                {
                    return BadRequest(new { message = "Masa, kullanıcı ve sipariş kalemleri zorunludur." });
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

                            // Ürün veya Menü fiyatını al
                            if (item.UrunId > 0)
                            {
                                var fiyatQuery = "SELECT BirimFiyati FROM Urun WHERE Id = @Id";
                                using (var cmd = new SqlCommand(fiyatQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Id", item.UrunId);
                                    var result = cmd.ExecuteScalar();
                                    if (result != null)
                                        birimFiyat = Convert.ToDecimal(result);
                                }
                            }
                            else if (item.MenuId > 0)
                            {
                                var fiyatQuery = "SELECT BirimFiyati FROM Menu WHERE Id = @Id";
                                using (var cmd = new SqlCommand(fiyatQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Id", item.MenuId);
                                    var result = cmd.ExecuteScalar();
                                    if (result != null)
                                        birimFiyat = Convert.ToDecimal(result);
                                }
                            }

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
                                cmd.Parameters.AddWithValue("@MenuId", item.MenuId > 0 ? (object)item.MenuId : DBNull.Value);
                                cmd.Parameters.AddWithValue("@UrunId", item.UrunId > 0 ? (object)item.UrunId : DBNull.Value);
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
                            cmd.Parameters.AddWithValue("@OdemeDurumu", (int)OdemeDurumu.Odenmedi);
                            cmd.Parameters.AddWithValue("@SiparisDurumu", (int)SiparisDurumu.Beklemede);
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
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sipariş oluşturulurken hata oluştu.", error = ex.Message });
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
        /// Kullanıcının kendi siparişlerini getir
        /// </summary>
        [HttpGet("my/{kullaniciId}")]
        public IActionResult GetMyOrders(int kullaniciId)
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
                                 WHERE s.KullaniciId = @KullaniciId
                                 ORDER BY s.Tarih DESC";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciId", kullaniciId);

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

        private string GenerateSatisKodu()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999).ToString();
        }
    }

    public class CreateOrderRequest
    {
        public int MasaId { get; set; }
        public int KullaniciId { get; set; }
        public decimal? IndirimOrani { get; set; }
        public string Aciklama { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public int UrunId { get; set; }
        public int MenuId { get; set; }
        public int Miktari { get; set; }
        public string Aciklama { get; set; }
    }

    public class PayOrderRequest
    {
        public int KullaniciId { get; set; }
        public string SatisKodu { get; set; }
        public string OdemeTuru { get; set; }
    }
}

