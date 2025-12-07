using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace RestoranOtomasyonu.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly string _connectionString;

        public MenuController(IConfiguration configuration)
        {
            _connectionString = "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true";
        }

        /// <summary>
        /// Tüm ürünleri getir
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

                    var query = @"SELECT Id, UrunAdi, BirimFiyati, Aciklama, Resim 
                                 FROM Urun 
                                 WHERE AktifMi = 1
                                 ORDER BY UrunAdi";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                urunler.Add(new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    urunAdi = reader.IsDBNull(reader.GetOrdinal("UrunAdi")) ? "" : reader.GetString(reader.GetOrdinal("UrunAdi")),
                                    birimFiyati = reader.IsDBNull(reader.GetOrdinal("BirimFiyati")) ? 0 : reader.GetDecimal(reader.GetOrdinal("BirimFiyati")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    resim = reader.IsDBNull(reader.GetOrdinal("Resim")) ? "" : reader.GetString(reader.GetOrdinal("Resim"))
                                });
                            }
                        }
                    }

                    // Menüleri de ekle
                    var menuQuery = @"SELECT Id, MenuAdi, BirimFiyati, Aciklama, Resim 
                                     FROM Menu 
                                     WHERE AktifMi = 1
                                     ORDER BY MenuAdi";

                    using (var cmd = new SqlCommand(menuQuery, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                urunler.Add(new
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    menuAdi = reader.IsDBNull(reader.GetOrdinal("MenuAdi")) ? "" : reader.GetString(reader.GetOrdinal("MenuAdi")),
                                    birimFiyati = reader.IsDBNull(reader.GetOrdinal("BirimFiyati")) ? 0 : reader.GetDecimal(reader.GetOrdinal("BirimFiyati")),
                                    aciklama = reader.IsDBNull(reader.GetOrdinal("Aciklama")) ? "" : reader.GetString(reader.GetOrdinal("Aciklama")),
                                    resim = reader.IsDBNull(reader.GetOrdinal("Resim")) ? "" : reader.GetString(reader.GetOrdinal("Resim")),
                                    isMenu = true
                                });
                            }
                        }
                    }
                }

                return Ok(new { success = true, data = urunler });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ürünler getirilirken hata oluştu.", error = ex.Message });
            }
        }
    }
}

