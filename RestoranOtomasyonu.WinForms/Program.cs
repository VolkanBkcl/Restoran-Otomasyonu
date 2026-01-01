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

            
            UserLookAndFeel.Default.SetSkinStyle("The Bezier");
            
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("Segoe UI", 9F);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            using (frmLogin loginForm = new frmLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    YetkiKontrolu.MevcutKullanici = loginForm.GirisYapanKullanici;
                    

                    
                    var anaMenu = new frmAnaMenu();
                    
                    string apiBaseUrl = "http://localhost:5146";
                    var signalRService = new SignalRClientService(apiBaseUrl);
                    
                    signalRService.OrderReceived += (sender, e) =>
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        MessageBox.Show($"Yeni sipariş geldi!\nMasa: {e.MasaId}\nSipariş Kodu: {e.SatisKodu}\nTutar: {e.NetTutar:C2}", 
                            "Yeni Sipariş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    
                    signalRService.OrderPaid += (sender, e) =>
                    {
                        System.Diagnostics.Debug.WriteLine($"Sipariş ödendi: {e.SiparisId}");
                    };
                    
                    Task.Run(async () => await signalRService.ConnectAsync());
                    
                    Application.Run(anaMenu);
                }
            }
        }
    }
}
