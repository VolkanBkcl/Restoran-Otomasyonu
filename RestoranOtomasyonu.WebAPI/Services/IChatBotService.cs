using RestoranOtomasyonu.WebAPI.Models;

namespace RestoranOtomasyonu.WebAPI.Services
{
    public interface IChatBotService
    {
        Task<ChatResponseDto> GetAiResponseAsync(ChatRequestDto request, CancellationToken cancellationToken = default);
    }
}
