namespace RestoranOtomasyonu.WinForms.Core
{
    /// <summary>
    /// Sistemde kullanılan rol sabitleri.
    /// Magic string kullanımını önlemek için oluşturulmuştur.
    /// </summary>
    public static class Roller
    {
        /// <summary>
        /// Yönetici rolü - Tüm yetkilere sahiptir
        /// </summary>
        public const string Yonetici = "Yönetici";

        /// <summary>
        /// Kasa rolü - Ödeme alma, indirim yapma yetkisi
        /// </summary>
        public const string Kasa = "Kasa";

        /// <summary>
        /// Garson rolü - Sipariş ekleme yetkisi
        /// </summary>
        public const string Garson = "Garson";

        /// <summary>
        /// Mutfak rolü - Sipariş görüntüleme yetkisi
        /// </summary>
        public const string Mutfak = "Mutfak";

        /// <summary>
        /// Müşteri rolü - Sadece kendi siparişlerini görme ve sipariş verme yetkisi
        /// </summary>
        public const string Musteri = "Musteri";

        /// <summary>
        /// Kurye rolü - Teslimat yetkisi
        /// </summary>
        public const string Kurye = "Kurye";

        /// <summary>
        /// Tüm rolleri içeren dizi
        /// </summary>
        public static readonly string[] TumRoller = new string[]
        {
            Yonetici,
            Kasa,
            Garson,
            Mutfak,
            Musteri,
            Kurye
        };

        /// <summary>
        /// Rolün geçerli olup olmadığını kontrol eder
        /// </summary>
        public static bool GecerliRol(string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
                return false;

            return System.Array.Exists(TumRoller, r => r.Equals(rol, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}

