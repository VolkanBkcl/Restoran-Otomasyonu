namespace RestoranOtomasyonu.WebAPI.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        /// <summary>
        /// "user", "ai", "operator" gibi değerler.
        /// </summary>
        public string Sender { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Bu mesaj canlı destek talebi mi?
        /// </summary>
        public bool IsLiveSupportRequest { get; set; }

        /// <summary>
        /// Aynı oturuma ait mesajları gruplayabilmek için.
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// Opsiyonel: Kullanıcı adı veya görünen isim.
        /// </summary>
        public string? UserName { get; set; }
    }
}
