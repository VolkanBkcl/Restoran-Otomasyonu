using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.DTOs;
using RestoranOtomasyonu.Entities.Enums;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Services;
using RestoranOtomasyonu.WinForms.Services;
using RestoranOtomasyonu.WinForms.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RestoranOtomasyonu.WinForms.SiparisYonetim
{
    public partial class frmSiparisYonetim : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private SiparisGrupService siparisGrupService = new SiparisGrupService();
        private SignalRClientService signalRService;
        private HttpClient httpClient;
        private string apiBaseUrl = "http://localhost:5146/api/order"; // WebAPI base URL
        
        // API URL'ini döndür (şimdilik sabit, ileride App.config'den okunabilir)
        private string GetApiBaseUrl()
        {
            // Varsayılan URL
            return "http://localhost:5146/api/order";
        }

        public frmSiparisYonetim()
        {
            InitializeComponent();
            
            // API URL'ini yapılandırmadan al
            apiBaseUrl = GetApiBaseUrl();
            
            InitializeSignalR();
            InitializeHttpClient();
            
            // Yetki kontrolü - Sadece Yönetici, Garson veya Mutfak erişebilir
            if (!YetkiKontrolu.YoneticiMi && !YetkiKontrolu.GarsonMi && !YetkiKontrolu.RolVarMi("Mutfak"))
            {
                XtraMessageBox.Show("Bu işlem için Yönetici, Garson veya Mutfak yetkisi gereklidir.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            
            // Durum Değiştir butonunu sadece Yönetici ve Mutfak görebilir
            bool durumDegistirmeYetkisi = YetkiKontrolu.YoneticiMi || YetkiKontrolu.RolVarMi("Mutfak");
            btnDurumDegistir.Visible = durumDegistirmeYetkisi;
            btnDurumDegistir.Enabled = durumDegistirmeYetkisi;
            
            Listele();
        }

        private void InitializeSignalR()
        {
            try
            {
                // SignalR servisini oluştur (Program.cs'de zaten bağlı olmalı)
                string apiBaseUrl = "http://localhost:5146";
                signalRService = new SignalRClientService(apiBaseUrl);
                // SignalR bağlantısı Program.cs'de yapılıyor, burada sadece referans tutuyoruz
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"SignalR bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeHttpClient()
        {
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
            // BaseAddress kullanmıyoruz, tam URL kullanacağız
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void Listele()
        {
            try
            {
                var gruplar = siparisGrupService.GetGruplandirilmisSiparisler(context);
                
                // DataTable oluştur
                var dt = new DataTable();
                dt.Columns.Add("GrupId", typeof(string));
                dt.Columns.Add("MasaAdi", typeof(string));
                dt.Columns.Add("ToplamTutar", typeof(decimal));
                dt.Columns.Add("NetTutar", typeof(decimal));
                dt.Columns.Add("SiparisSayisi", typeof(int));
                dt.Columns.Add("KullaniciSayisi", typeof(int));
                dt.Columns.Add("Durum", typeof(int));
                dt.Columns.Add("DurumMetni", typeof(string));
                dt.Columns.Add("IlkSiparisTarihi", typeof(DateTime));
                dt.Columns.Add("SonSiparisTarihi", typeof(DateTime));

                foreach (var grup in gruplar)
                {
                    dt.Rows.Add(
                        grup.GrupId,
                        grup.MasaAdi,
                        grup.ToplamTutar,
                        grup.NetTutar,
                        grup.SiparisSayisi,
                        grup.KullaniciSayisi,
                        (int)grup.Durum,
                        grup.DurumMetni,
                        grup.IlkSiparisTarihi,
                        grup.SonSiparisTarihi
                    );
                }

                gridControlSiparisler.DataSource = dt;
                gridViewSiparisler.BestFitColumns();

                // Durum kolonuna renk ver
                if (gridViewSiparisler.Columns["DurumMetni"] != null)
                {
                    gridViewSiparisler.Columns["DurumMetni"].AppearanceCell.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Siparişler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnDetaylar_Click(object sender, EventArgs e)
        {
            var focusedRow = gridViewSiparisler.GetFocusedRow();
            if (focusedRow == null)
            {
                XtraMessageBox.Show("Lütfen bir sipariş grubu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var grupId = gridViewSiparisler.GetFocusedRowCellValue("GrupId")?.ToString();
            if (string.IsNullOrEmpty(grupId))
                return;

            // Grup detaylarını göster
            var gruplar = siparisGrupService.GetGruplandirilmisSiparisler(context);
            var grup = gruplar.FirstOrDefault(g => g.GrupId == grupId);

            if (grup == null)
            {
                XtraMessageBox.Show("Grup bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Detay formunu aç
            var frmDetay = new frmSiparisDetay(grup);
            frmDetay.ShowDialog();
        }

        private void btnDurumDegistir_Click(object sender, EventArgs e)
        {
            var focusedRow = gridViewSiparisler.GetFocusedRow();
            if (focusedRow == null)
            {
                XtraMessageBox.Show("Lütfen bir sipariş grubu seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var grupId = gridViewSiparisler.GetFocusedRowCellValue("GrupId")?.ToString();
            var mevcutDurum = Convert.ToInt32(gridViewSiparisler.GetFocusedRowCellValue("Durum"));

            // Durum seçim formu
            var frmDurum = new frmSiparisDurumSec(mevcutDurum);
            if (frmDurum.ShowDialog() == DialogResult.OK)
            {
                var yeniDurum = frmDurum.SecilenDurum;
                DurumGuncelle(grupId, yeniDurum);
            }
        }

        private async void DurumGuncelle(string grupId, SiparisDurumu yeniDurum)
        {
            try
            {
                // GrupId kontrolü
                if (string.IsNullOrEmpty(grupId))
                {
                    XtraMessageBox.Show("Grup ID bulunamadı. Lütfen bir sipariş grubu seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // WebAPI'ye istek gönder
                var request = new
                {
                    GrupId = grupId,
                    SiparisDurumu = (int)yeniDurum
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // URL'yi oluştur
                var url = $"{apiBaseUrl}/updateGroupStatus";
                
                var response = await httpClient.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    XtraMessageBox.Show("Sipariş durumu başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele(); // Listeyi yenile
                }
                else
                {
                    // Hata mesajını parse et
                    string hataMesaji = responseContent;
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        if (errorObj?.message != null)
                            hataMesaji = errorObj.message.ToString();
                        else if (errorObj?.error != null)
                            hataMesaji = errorObj.error.ToString();
                    }
                    catch
                    {
                        // JSON parse edilemezse direkt mesajı göster
                    }

                    XtraMessageBox.Show(
                        $"Durum güncellenirken hata oluştu:\n\n" +
                        $"HTTP Durum: {response.StatusCode} ({(int)response.StatusCode})\n" +
                        $"Hata: {hataMesaji}\n\n" +
                        $"Grup ID: {grupId}\n" +
                        $"Yeni Durum: {yeniDurum}",
                        "Hata", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                string detayMesaj = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                
                XtraMessageBox.Show(
                    $"WebAPI'ye bağlanılamadı!\n\n" +
                    $"Hata: {detayMesaj}\n\n" +
                    $"API URL: {apiBaseUrl}\n\n" +
                    $"Çözüm Önerileri:\n" +
                    $"1. Visual Studio'da 'RestoranOtomasyonu.WebAPI' projesini çalıştırın\n" +
                    $"2. WebAPI'nin http://localhost:5146 adresinde çalıştığını kontrol edin\n" +
                    $"3. Tarayıcıda http://localhost:5146/swagger adresini açarak API'nin çalıştığını doğrulayın\n" +
                    $"4. Firewall veya antivirüs yazılımının bağlantıyı engellemediğinden emin olun",
                    "Bağlantı Hatası", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            catch (TaskCanceledException ex)
            {
                XtraMessageBox.Show(
                    $"İstek zaman aşımına uğradı:\n\n{ex.Message}\n\n" +
                    $"Lütfen internet bağlantınızı kontrol edin.",
                    "Zaman Aşımı", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Stack trace'i güvenli şekilde al
                string stackTraceInfo = "N/A";
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    int maxLength = Math.Min(200, ex.StackTrace.Length);
                    stackTraceInfo = ex.StackTrace.Substring(0, maxLength) + "...";
                }

                XtraMessageBox.Show(
                    $"Durum güncellenirken beklenmeyen bir hata oluştu:\n\n{ex.Message}\n\n" +
                    $"Hata Tipi: {ex.GetType().Name}\n" +
                    $"Stack Trace: {stackTraceInfo}",
                    "Hata", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSiparisYonetim_FormClosing(object sender, FormClosingEventArgs e)
        {
            httpClient?.Dispose();
        }
    }
}
