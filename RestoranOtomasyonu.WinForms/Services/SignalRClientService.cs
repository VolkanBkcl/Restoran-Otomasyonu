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

    /// </summary>
    public class SignalRClientService
    {
        private readonly string _hubUrl;
        private bool _isConnected = false;

        /// <summary>

        /// </summary>
        public event EventHandler<OrderReceivedEventArgs> OrderReceived;

        /// <summary>

        /// </summary>
        public event EventHandler<OrderPaidEventArgs> OrderPaid;

        public SignalRClientService(string apiBaseUrl)
        {
            _hubUrl = apiBaseUrl;
        }

        /// <summary>

        /// </summary>
        public Task ConnectAsync()
        {
            _isConnected = true;
            System.Diagnostics.Debug.WriteLine($"[SignalRClientService] Stub bağlantı kuruldu. HubUrl: {_hubUrl}");
            return Task.CompletedTask;
        }

        /// <summary>

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

