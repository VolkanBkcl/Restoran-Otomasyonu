using Microsoft.AspNetCore.Mvc;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Enums;
using System;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace RestoranOtomasyonu.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            // Connection string'i appsettings'den al
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true";
            _logger = logger;
        }

        /// <summary>
        /// Müşteri kaydı
        /// </summary>
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.KullaniciAdi) || string.IsNullOrWhiteSpace(request.Parola))
                {
                    return BadRequest(new { message = "Kullanıcı adı ve parola zorunludur." });
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Kullanıcı adı kontrolü
                    var checkQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
                    using (var checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@KullaniciAdi", request.KullaniciAdi);
                        var exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            return BadRequest(new { message = "Bu kullanıcı adı zaten kullanılıyor." });
                        }
                    }

                    // Parolayı hash'leme (WinForms ile uyumlu olması için - production'da hash kullanılmalı)
                    // Not: WinForms uygulaması parolayı hash'lemeden kaydediyor, bu yüzden burada da hash'lemiyoruz
                    // Production ortamında mutlaka SHA256 veya bcrypt kullanın!

                    // Yeni kullanıcı ekle
                    var insertQuery = @"INSERT INTO Kullanicilar 
                        (AdSoyad, Telefon, Email, Gorevi, KullaniciAdi, Parola, HatirlatmaSorusu, Cevap, KayitTarihi, AktifMi)
                        VALUES 
                        (@AdSoyad, @Telefon, @Email, @Gorevi, @KullaniciAdi, @Parola, @HatirlatmaSorusu, @Cevap, @KayitTarihi, @AktifMi);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

                    using (var cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@AdSoyad", request.AdSoyad ?? "");
                        cmd.Parameters.AddWithValue("@Telefon", request.Telefon ?? "");
                        cmd.Parameters.AddWithValue("@Email", request.Email ?? "");
                        cmd.Parameters.AddWithValue("@Gorevi", "Musteri"); // Müşteri rolü
                        cmd.Parameters.AddWithValue("@KullaniciAdi", request.KullaniciAdi);
                        cmd.Parameters.AddWithValue("@Parola", request.Parola); // Hash'lenmemiş parola
                        cmd.Parameters.AddWithValue("@HatirlatmaSorusu", request.HatirlatmaSorusu ?? "");
                        cmd.Parameters.AddWithValue("@Cevap", request.Cevap ?? "");
                        cmd.Parameters.AddWithValue("@KayitTarihi", DateTime.Now);
                        cmd.Parameters.AddWithValue("@AktifMi", true);

                        var userId = (int)cmd.ExecuteScalar();

                        return Ok(new
                        {
                            success = true,
                            message = "Kayıt başarılı.",
                            userId = userId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kayıt sırasında hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Giriş yap
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.KullaniciAdi) || string.IsNullOrWhiteSpace(request.Parola))
                {
                    return BadRequest(new { message = "Kullanıcı adı ve parola zorunludur." });
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Önce hash'lenmemiş parola ile kontrol et (WinForms uyumluluğu için)
                    // Sonra hash'lenmiş parola ile kontrol et (eski kayıtlar için)
                    var query = @"SELECT Id, AdSoyad, Telefon, Email, Gorevi, KullaniciAdi, Parola, AktifMi 
                                 FROM Kullanicilar 
                                 WHERE KullaniciAdi = @KullaniciAdi";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciAdi", request.KullaniciAdi);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var dbParola = reader.IsDBNull(reader.GetOrdinal("Parola")) ? "" : reader.GetString(reader.GetOrdinal("Parola"));
                                var hashedPassword = HashPassword(request.Parola);
                                
                                // Hem hash'lenmemiş hem hash'lenmiş parolayı kontrol et
                                bool parolaDogru = dbParola == request.Parola || dbParola == hashedPassword;
                                
                                if (!parolaDogru)
                                {
                                    _logger.LogWarning($"Giriş başarısız: Kullanıcı adı={request.KullaniciAdi}");
                                    return Unauthorized(new { message = "Kullanıcı adı veya parola hatalı." });
                                }

                                var aktifMi = reader.GetBoolean(reader.GetOrdinal("AktifMi"));
                                if (!aktifMi)
                                {
                                    return Unauthorized(new { message = "Hesabınız pasif durumda." });
                                }

                                _logger.LogInformation($"Giriş başarılı: Kullanıcı adı={request.KullaniciAdi}");

                                return Ok(new
                                {
                                    success = true,
                                    token = Guid.NewGuid().ToString(), // Basit token (production'da JWT kullanın)
                                    user = new
                                    {
                                        id = reader.GetInt32(reader.GetOrdinal("Id")),
                                        adSoyad = reader.IsDBNull(reader.GetOrdinal("AdSoyad")) ? "" : reader.GetString(reader.GetOrdinal("AdSoyad")),
                                        kullaniciAdi = reader.GetString(reader.GetOrdinal("KullaniciAdi")),
                                        gorevi = reader.IsDBNull(reader.GetOrdinal("Gorevi")) ? "" : reader.GetString(reader.GetOrdinal("Gorevi")),
                                        email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                                        telefon = reader.IsDBNull(reader.GetOrdinal("Telefon")) ? "" : reader.GetString(reader.GetOrdinal("Telefon"))
                                    }
                                });
                            }
                            else
                            {
                                _logger.LogWarning($"Kullanıcı bulunamadı: Kullanıcı adı={request.KullaniciAdi}");
                                return Unauthorized(new { message = "Kullanıcı adı veya parola hatalı." });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Giriş sırasında hata oluştu.", error = ex.Message });
            }
        }

        private string HashPassword(string password)
        {
            // Basit MD5 hash (production'da SHA256 veya bcrypt kullanın)
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = md5.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }

    public class RegisterRequest
    {
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string HatirlatmaSorusu { get; set; }
        public string Cevap { get; set; }
    }

    public class LoginRequest
    {
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
    }
}

