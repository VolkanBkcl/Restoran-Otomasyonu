using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RestoranOtomasyonu.WinForms.Services
{
    /// <summary>
    /// SignalR Client Service - Web API'den gelen gerçek zamanlı sipariş bildirimlerini dinler
    /// </summary>
    public class SignalRClientService
    {
        private HubConnection _connection;
        private string _hubUrl;
        private bool _isConnected = false;

        public event EventHandler<OrderReceivedEventArgs> OrderReceived;
        public event EventHandler<OrderPaidEventArgs> OrderPaid;

        public SignalRClientService(string apiBaseUrl)
        {
            _hubUrl = $"{apiBaseUrl}/siparisHub";
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30) })
                .Build();

            // Yeni sipariş geldiğinde
            _connection.On<dynamic>("ReceiveOrder", (orderData) =>
            {
                OrderReceived?.Invoke(this, new OrderReceivedEventArgs
                {
                    SiparisId = orderData.siparisId,
                    MasaId = orderData.masaId,
                    KullaniciId = orderData.kullaniciId,
                    SatisKodu = orderData.satisKodu?.ToString() ?? "",
                    ToplamTutar = Convert.ToDecimal(orderData.toplamTutar),
                    NetTutar = Convert.ToDecimal(orderData.netTutar),
                    Tarih = Convert.ToDateTime(orderData.tarih)
                });
            });

            // Sipariş ödendiğinde
            _connection.On<dynamic>("OrderPaid", (paymentData) =>
            {
                OrderPaid?.Invoke(this, new OrderPaidEventArgs
                {
                    SiparisId = paymentData.siparisId,
                    KullaniciId = paymentData.kullaniciId,
                    OdenenTutar = Convert.ToDecimal(paymentData.odenenTutar)
                });
            });

            // Bağlantı durumu değişiklikleri
            _connection.Closed += async (error) =>
            {
                _isConnected = false;
                if (error != null)
                {
                    System.Diagnostics.Debug.WriteLine($"SignalR bağlantısı kesildi: {error.Message}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("SignalR bağlantısı normal olarak kapatıldı.");
                }
            };

            _connection.Reconnecting += (error) =>
            {
                _isConnected = false;
                string errorMsg = error != null ? error.Message : "Bilinmeyen hata";
                System.Diagnostics.Debug.WriteLine($"SignalR yeniden bağlanıyor... Hata: {errorMsg}");
                return Task.CompletedTask;
            };

            _connection.Reconnected += (connectionId) =>
            {
                _isConnected = true;
                System.Diagnostics.Debug.WriteLine($"SignalR yeniden bağlandı! ConnectionId: {connectionId}");
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// SignalR Hub'a bağlan
        /// </summary>
        public async Task ConnectAsync()
        {
            if (_isConnected) return;

            try
            {
                System.Diagnostics.Debug.WriteLine($"SignalR bağlantısı deneniyor: {_hubUrl}");
                await _connection.StartAsync();
                _isConnected = true;
                System.Diagnostics.Debug.WriteLine($"SignalR bağlantısı başarılı! ConnectionId: {_connection.ConnectionId}");
            }
            catch (Exception ex)
            {
                _isConnected = false;
                string errorMessage = $"SignalR bağlantı hatası: {ex.Message}\n\nHub URL: {_hubUrl}\n\nWeb API'nin çalıştığından emin olun.";
                System.Diagnostics.Debug.WriteLine(errorMessage);
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                
                // Kullanıcıya sadece ilk bağlantı hatasında göster (arka planda retry yapılacak)
                // XtraMessageBox.Show(errorMessage, "SignalR Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// SignalR Hub bağlantısını kes
        /// </summary>
        public async Task DisconnectAsync()
        {
            if (!_isConnected) return;

            try
            {
                await _connection.StopAsync();
                _isConnected = false;
                System.Diagnostics.Debug.WriteLine("SignalR bağlantısı kesildi.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SignalR bağlantı kesme hatası: {ex.Message}");
            }
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

