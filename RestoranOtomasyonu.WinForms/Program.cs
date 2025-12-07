using RestoranOtomasyonu.WinForms.AnaMenu;
using RestoranOtomasyonu.WinForms.Login;
using RestoranOtomasyonu.WinForms.Core;
using RestoranOtomasyonu.WinForms.Services;
using RollerSabitleri = RestoranOtomasyonu.WinForms.Core.Roller;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // DevExpress Office Skin'lerini yükle (eğer mevcut ise)
            try
            {
                OfficeSkins.Register();
            }
            catch
            {
                // OfficeSkins mevcut değilse devam et
            }
            
            // Modern tema ayarla - "The Bezier" (2024+ modern ve şık görünüm)
            // Alternatifler: "Office 2019 Colorful", "Office 2019 Black", "WXI", "VS2019"
            UserLookAndFeel.Default.SetSkinStyle("The Bezier");
            
            // Global font ayarları (tüm formlara uygulanacak)
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("Segoe UI", 9F);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Login formunu göster
            using (frmLogin loginForm = new frmLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Giriş yapan kullanıcıyı YetkiKontrolu'na set et
                    YetkiKontrolu.MevcutKullanici = loginForm.GirisYapanKullanici;
                    
                    // DEBUG: Giriş yapan kullanıcı bilgilerini logla
                    if (YetkiKontrolu.MevcutKullanici != null)
                    {
                        System.Diagnostics.Debug.WriteLine("=== KULLANICI GİRİŞ BİLGİLERİ ===");
                        System.Diagnostics.Debug.WriteLine($"Kullanıcı Adı: {YetkiKontrolu.MevcutKullanici.KullaniciAdi}");
                        System.Diagnostics.Debug.WriteLine($"Ad Soyad: {YetkiKontrolu.MevcutKullanici.AdSoyad}");
                        System.Diagnostics.Debug.WriteLine($"Görevi (Ham): '{YetkiKontrolu.MevcutKullanici.Gorevi}'");
                        System.Diagnostics.Debug.WriteLine($"Görevi (Trim): '{YetkiKontrolu.MevcutKullanici.Gorevi?.Trim()}'");
                        System.Diagnostics.Debug.WriteLine($"Görevi (Normalize): '{YetkiKontrolu.MevcutKullaniciGorevi}'");
                        System.Diagnostics.Debug.WriteLine($"Yönetici Mi?: {YetkiKontrolu.YoneticiMi}");
                        System.Diagnostics.Debug.WriteLine($"Roller.Yonetici Sabiti: '{RollerSabitleri.Yonetici}'");
                        System.Diagnostics.Debug.WriteLine("================================");
                    }
                    
                    // Giriş başarılı - Ana menüyü aç
                    var anaMenu = new frmAnaMenu();
                    
                    // SignalR Client'ı başlat (Web API URL'i - appsettings veya varsayılan)
                    // Web API varsayılan portu: 5146 (http) veya 7247 (https)
                    // Eğer Web API çalışmıyorsa, bu bağlantı sessizce başarısız olur
                    string apiBaseUrl = "http://localhost:5146"; // Web API'nin çalıştığı port
                    var signalRService = new SignalRClientService(apiBaseUrl);
                    
                    // Sipariş bildirimlerini dinle
                    signalRService.OrderReceived += (sender, e) =>
                    {
                        // Sesli uyarı ve bildirim
                        System.Media.SystemSounds.Exclamation.Play();
                        MessageBox.Show($"Yeni sipariş geldi!\nMasa: {e.MasaId}\nSipariş Kodu: {e.SatisKodu}\nTutar: {e.NetTutar:C2}", 
                            "Yeni Sipariş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    
                    signalRService.OrderPaid += (sender, e) =>
                    {
                        System.Diagnostics.Debug.WriteLine($"Sipariş ödendi: {e.SiparisId}");
                    };
                    
                    // SignalR'ye bağlan (async olarak)
                    Task.Run(async () => await signalRService.ConnectAsync());
                    
                    Application.Run(anaMenu);
                }
            }
        }
    }
}
