using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text;

namespace RestoranOtomasyonu.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IConfiguration configuration, ILogger<MenuController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true";
            _logger = logger;
        }

        /// <summary>
        /// Tüm kategorileri (Menu) getir
        /// </summary>
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            try
            {
                var kategoriler = new List<object>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Menu tablosu kategorileri temsil ediyor
                    var query = @"SELECT DISTINCT m.Id, m.MenuAdi, m.Aciklama,
                                 (SELECT COUNT(*) FROM Urun u WHERE u.MenuId = m.Id) as UrunSayisi
                                 FROM Menu m
                                 WHERE EXISTS (SELECT 1 FROM Urun u WHERE u.MenuId = m.Id)
                                 ORDER BY m.MenuAdi";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                kategoriler.Add(new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    menuAdi = reader.IsDBNull(reader.GetOrdinal("MenuAdi")) ? "" : reader.GetString(reader.GetOrdinal("MenuAdi")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    urunSayisi = reader.GetInt32(reader.GetOrdinal("UrunSayisi"))
                                });
                            }
                        }
                    }
                }

                return Ok(new { success = true, data = kategoriler });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategoriler getirilirken hata oluştu");
                return StatusCode(500, new { message = "Kategoriler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Belirli bir kategoriye ait ürünleri getir (BirimFiyati2 kullanılır - müşteri fiyatı)
        /// </summary>
        [HttpGet("products/{kategoriId}")]
        public IActionResult GetProductsByCategory(int kategoriId)
        {
            try
            {
                var urunler = new List<object>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // BirimFiyati2 müşteri fiyatı olarak kullanılıyor
                    var query = @"SELECT u.Id, u.UrunAdi, u.BirimFiyati2 as BirimFiyati, u.Aciklama, u.Resim, u.MenuId,
                                 m.MenuAdi as KategoriAdi
                                 FROM Urun u
                                 INNER JOIN Menu m ON u.MenuId = m.Id
                                 WHERE u.MenuId = @KategoriId
                                 ORDER BY u.UrunAdi";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@KategoriId", kategoriId);
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var resimObj = reader.IsDBNull(reader.GetOrdinal("Resim")) ? null : reader.GetValue(reader.GetOrdinal("Resim"));

                                urunler.Add(new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    urunAdi = reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? "" : reader.GetString(reader.GetOrdinal("UrunAdi")),
                                    birimFiyati = reader.IsDBNull(reader.GetOrdinal("BirimFiyati")) ? 0 : reader.GetDecimal(reader.GetOrdinal("BirimFiyati")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    resim = NormalizeImage(resimObj),
                                    kategoriId = reader.GetInt32(reader.GetOrdinal("MenuId")),
                                    kategoriAdi = reader.IsDBNull(reader.GetOrdinal("KategoriAdi")) ? "" : reader.GetString(reader.GetOrdinal("KategoriAdi")),
                                    isMenu = false
                                });
                            }
                        }
                    }
                }

                return Ok(new { success = true, data = urunler });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kategori {kategoriId} için ürünler getirilirken hata oluştu");
                return StatusCode(500, new { message = "Ürünler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Tüm ürünleri getir (geriye dönük uyumluluk için)
        /// </summary>
        [HttpGet]
        public IActionResult GetMenu()
        {
            try
            {
                var urunler = new List<object>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // BirimFiyati2 müşteri fiyatı olarak kullanılıyor
                    var query = @"SELECT u.Id, u.UrunAdi, u.BirimFiyati2 as BirimFiyati, u.Aciklama, u.Resim, u.MenuId,
                                 m.MenuAdi as KategoriAdi
                                 FROM Urun u
                                 INNER JOIN Menu m ON u.MenuId = m.Id
                                 ORDER BY m.MenuAdi, u.UrunAdi";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var resimObj = reader.IsDBNull(reader.GetOrdinal("Resim")) ? null : reader.GetValue(reader.GetOrdinal("Resim"));

                                urunler.Add(new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    urunAdi = reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? "" : reader.GetString(reader.GetOrdinal("UrunAdi")),
                                    birimFiyati = reader.IsDBNull(reader.GetOrdinal("BirimFiyati")) ? 0 : reader.GetDecimal(reader.GetOrdinal("BirimFiyati")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    resim = NormalizeImage(resimObj),
                                    kategoriId = reader.GetInt32(reader.GetOrdinal("MenuId")),
                                    kategoriAdi = reader.IsDBNull(reader.GetOrdinal("KategoriAdi")) ? "" : reader.GetString(reader.GetOrdinal("KategoriAdi")),
                                    isMenu = false
                                });
                            }
                        }
                    }
                }

                return Ok(new { success = true, data = urunler });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Menü getirilirken hata oluştu");
                return StatusCode(500, new { message = "Ürünler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        private string NormalizeImage(object? value)
        {
            if (value == null || value is DBNull) return "";

            if (value is string s)
            {
                // String ise (dosya yolu veya data uri)
                if (string.IsNullOrWhiteSpace(s)) return "";
                
                // Eğer data URI değilse (http, https, data: ile başlamıyorsa)
                // Windows path backslash'larını URL için forward slash'a çevir
                if (!s.StartsWith("http", StringComparison.OrdinalIgnoreCase) && 
                    !s.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                {
                    // Backslash'ları forward slash'a çevir (URL uyumluluğu için)
                    s = s.Replace('\\', '/');
                }
                
                return s;
            }

            if (value is byte[] bytes && bytes.Length > 0)
            {
                // Varsayılan PNG; gerekirse içerikten MIME tespiti eklenebilir
                var base64 = Convert.ToBase64String(bytes);
                return $"data:image/png;base64,{base64}";
            }

            return "";
        }
    }
}

