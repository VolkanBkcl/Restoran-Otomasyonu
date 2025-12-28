namespace RestoranOtomasyonu.WebAPI.Models
{
    public class ChatRequestDto
    {
        public string Message { get; set; } = string.Empty;
        public bool IsLiveSupportRequest { get; set; }
        public string? SessionId { get; set; }
        public string? UserName { get; set; }
    }

    public class ChatResponseDto
    {
        public string Reply { get; set; } = string.Empty;
        public bool FromLiveSupport { get; set; }
        public string? DebugMenuContext { get; set; }
    }

    public class ChatMenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Ingredients { get; set; } = string.Empty;
        public int? Calories { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
