using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RestoranOtomasyonu.WinForms.Services
{
    /// <summary>
    /// SignalR Client Service - Web API'den gelen gerçek zamanlı sipariş bildirimlerini dinlemek için
    /// tasarlanmış servis sınıfı.
    /// 
    /// NOT:
    /// Orijinal sürüm Microsoft.AspNetCore.SignalR.Client kütüphanesine bağlıydı.
    /// Bu kütüphane derleme hatalarına neden olduğu için, servis şu anda "stub"
    /// (boş ancak imzası korunan) hale getirildi. Böylece Program.cs içindeki
    /// kullanım derlenir, ancak gerçek zamanlı bağlantı kurulmaz.
    /// İleride ihtiyaç duyarsanız, gerçek SignalR kodunu tekrar ekleyebilirsiniz.
    /// </summary>
    public class SignalRClientService
    {
        private readonly string _hubUrl;
        private bool _isConnected = false;

        /// <summary>
        /// Yeni sipariş geldiğinde tetiklenecek event (şu anda yalnızca manuel kullanılabilir).
        /// </summary>
        public event EventHandler<OrderReceivedEventArgs> OrderReceived;

        /// <summary>
        /// Sipariş ödendiğinde tetiklenecek event.
        /// </summary>
        public event EventHandler<OrderPaidEventArgs> OrderPaid;

        public SignalRClientService(string apiBaseUrl)
        {
            _hubUrl = apiBaseUrl; // Örn: http://localhost:5146
        }

        /// <summary>
        /// SignalR Hub'a bağlan (stub - gerçek bağlantı yok).
        /// </summary>
        public Task ConnectAsync()
        {
            _isConnected = true;
            System.Diagnostics.Debug.WriteLine($"[SignalRClientService] Stub bağlantı kuruldu. HubUrl: {_hubUrl}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// SignalR Hub bağlantısını kes (stub - gerçek bağlantı yok).
        /// </summary>
        public Task DisconnectAsync()
        {
            _isConnected = false;
            System.Diagnostics.Debug.WriteLine("[SignalRClientService] Stub bağlantı kesildi.");
            return Task.CompletedTask;
        }

        public bool IsConnected => _isConnected;
    }

    public class OrderReceivedEventArgs : EventArgs
    {
        public int SiparisId { get; set; }
        public int MasaId { get; set; }
        public int KullaniciId { get; set; }
        public string SatisKodu { get; set; }
        public decimal ToplamTutar { get; set; }
        public decimal NetTutar { get; set; }
        public DateTime Tarih { get; set; }
    }

    public class OrderPaidEventArgs : EventArgs
    {
        public int SiparisId { get; set; }
        public int KullaniciId { get; set; }
        public decimal OdenenTutar { get; set; }
    }
}

