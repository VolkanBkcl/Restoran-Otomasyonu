using Microsoft.AspNetCore.SignalR;

namespace RestoranOtomasyonu.WebAPI.Hubs
{
    /// <summary>
    /// SignalR Hub - Masaüstü uygulamasına gerçek zamanlı sipariş bildirimleri gönderir
    /// </summary>
    public class SiparisHub : Hub
    {
        /// <summary>
        /// Client bağlandığında
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
        }

        /// <summary>
        /// Client bağlantısı kesildiğinde
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
        }

        /// <summary>
        /// Belirli bir masaya bağlan (opsiyonel - masa bazlı bildirimler için)
        /// </summary>
        public async Task JoinMasaGroup(int masaId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Masa_{masaId}");
        }

        /// <summary>
        /// Masadan ayrıl
        /// </summary>
        public async Task LeaveMasaGroup(int masaId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Masa_{masaId}");
        }
    }
}

