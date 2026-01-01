using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.Models;
using KullanicilarEntity = RestoranOtomasyonu.Entities.Models.Kullanicilar;
using RollerSabitleri = RestoranOtomasyonu.WinForms.Core.Roller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Core
{
    /// <summary>
    /// Rol bazlı yetki kontrolü için merkezi yönetim sınıfı
    /// </summary>
    public static class YetkiKontrolu
    {
        /// <summary>
        /// Mevcut giriş yapan kullanıcı (Login formundan set edilir)
        /// </summary>
        public static KullanicilarEntity MevcutKullanici { get; set; }

        /// <summary>
        /// Kullanıcının görevini döndürür (trim edilmiş ve normalize edilmiş)
        /// </summary>
        public static string MevcutKullaniciGorevi => MevcutKullanici?.Gorevi?.Trim() ?? string.Empty;

        /// <summary>
        /// Kullanıcının belirtilen role sahip olup olmadığını kontrol eder
        /// </summary>
        public static bool RolVarMi(string rol)
        {
            if (MevcutKullanici == null || string.IsNullOrWhiteSpace(rol))
                return false;

            string kullaniciGorevi = MevcutKullaniciGorevi.Trim();
            string arananRol = rol.Trim();
            

            return string.Equals(kullaniciGorevi, arananRol, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Kullanıcının belirtilen rollerden herhangi birine sahip olup olmadığını kontrol eder
        /// </summary>
        public static bool RollerdenBiriVarMi(params string[] roller)
        {
            if (MevcutKullanici == null || roller == null || roller.Length == 0)
                return false;

            string kullaniciGorevi = MevcutKullaniciGorevi.Trim();
            return roller.Any(r => string.Equals(kullaniciGorevi, r?.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Yönetici kontrolü
        /// </summary>
        public static bool YoneticiMi => RolVarMi(RollerSabitleri.Yonetici);

        /// <summary>
        /// Kasa kontrolü
        /// </summary>
        public static bool KasaMi => RolVarMi(RollerSabitleri.Kasa);

        /// <summary>
        /// Garson kontrolü
        /// </summary>
        public static bool GarsonMi => RolVarMi(RollerSabitleri.Garson);

        /// <summary>
        /// Müşteri kontrolü
        /// </summary>
        public static bool MusteriMi => RolVarMi(RollerSabitleri.Musteri);

        /// <summary>
        /// Form üzerindeki kontrolleri rol bazlı olarak yapılandırır
        /// </summary>
        /// <param name="form">Yapılandırılacak form</param>
        /// <param name="kullaniciGorevi">Kullanıcının görevi</param>
        public static void FormYetkileriniAyarla(XtraForm form, string kullaniciGorevi)
        {
            if (form == null || string.IsNullOrWhiteSpace(kullaniciGorevi))
                return;


            var kontroller = form.Controls.Cast<Control>().ToList();
            var tumKontroller = KontrolleriTopla(kontroller);


            switch (kullaniciGorevi)
            {
                case "Yönetici":

                    break;

                case "Kasa":
                    KontrolGizleVeyaPasifYap(tumKontroller, new[] { "btnSil", "btnIptalEt" }, false);
                    break;

                case "Garson":
                    KontrolGizleVeyaPasifYap(tumKontroller, new[] { "btnOdemeAl", "txtIndirimOrani", "calcIndirimTutari", "btnIndirimYap", "btnSil", "btnIptalEt" }, true);
                    break;

                case "Musteri":
                    KontrolGizleVeyaPasifYap(tumKontroller, new[] { 
                        "btnOdemeAl", "txtIndirimOrani", "calcIndirimTutari", "btnIndirimYap", 
                        "btnSil", "btnIptalEt", "btnDuzenle", "btnExport", "btnRapor" 
                    }, true);
                    break;

                case "Mutfak":
                    KontrolGizleVeyaPasifYap(tumKontroller, new[] { 
                        "btnKaydet", "btnYeni", "btnDuzenle", "btnSil", 
                        "btnOdemeAl", "btnIndirimYap", "btnIptalEt" 
                    }, true);
                    break;

                case "Kurye":
                    KontrolGizleVeyaPasifYap(tumKontroller, new[] { 
                        "btnSil", "btnDuzenle", "btnIndirimYap", "btnRapor" 
                    }, false);
                    break;
            }
        }

        /// <summary>
        /// Belirli kontrolleri gizler veya pasif yapar
        /// </summary>
        private static void KontrolGizleVeyaPasifYap(List<Control> kontroller, string[] kontrolIsimleri, bool gizle)
        {
            foreach (var kontrol in kontroller)
            {
                if (kontrolIsimleri.Any(name => kontrol.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    if (gizle)
                    {
                        kontrol.Visible = false;
                    }
                    else
                    {
                        if (kontrol is SimpleButton btn)
                            btn.Enabled = false;
                        else if (kontrol is TextEdit txt)
                            txt.Enabled = false;
                        else if (kontrol is CalcEdit calc)
                            calc.Enabled = false;
                        else
                            kontrol.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Tüm kontrolleri recursive olarak toplar
        /// </summary>
        private static List<Control> KontrolleriTopla(List<Control> kontroller)
        {
            var tumKontroller = new List<Control>(kontroller);

            foreach (var kontrol in kontroller)
            {
                if (kontrol.HasChildren)
                {
                    tumKontroller.AddRange(KontrolleriTopla(kontrol.Controls.Cast<Control>().ToList()));
                }
            }

            return tumKontroller;
        }

        /// <summary>
        /// Belirli bir butonu rol bazlı olarak yapılandırır
        /// </summary>
        public static void ButonYetkisiAyarla(SimpleButton buton, string[] izinVerilenRoller, bool gizle = false)
        {
            if (buton == null || izinVerilenRoller == null || izinVerilenRoller.Length == 0)
                return;

            bool yetkiVar = RollerdenBiriVarMi(izinVerilenRoller);

            if (gizle)
            {
                buton.Visible = yetkiVar;
            }
            else
            {
                buton.Enabled = yetkiVar;
                if (!yetkiVar)
                {
                    buton.Appearance.ForeColor = System.Drawing.Color.Gray;
                }
            }
        }

        /// <summary>
        /// Belirli bir kontrolü rol bazlı olarak yapılandırır
        /// </summary>
        public static void KontrolYetkisiAyarla(Control kontrol, string[] izinVerilenRoller, bool gizle = false)
        {
            if (kontrol == null || izinVerilenRoller == null || izinVerilenRoller.Length == 0)
                return;

            bool yetkiVar = RollerdenBiriVarMi(izinVerilenRoller);

            if (gizle)
            {
                kontrol.Visible = yetkiVar;
            }
            else
            {
                kontrol.Enabled = yetkiVar;
            }
        }
    }
}

