namespace JWT.Class.GlobalClass
{
    /// <summary>
    /// Oluşturulan Token Çalındıında Extra Güvenlik Mekanizması İçin Oluşturulmuştur.Tokeni Çalan kişinin IP adresi, tarayıcı bilgisi ve timestamp bilgileri burada kontrol edilir.
    /// Bilgilerden herhangi biri tutarsız olursa extra yazacaqğınız güvenlik mekanizması kodlarıyla token hırsızlıını durdurabilirsiniz. Tokeni çalan kişi tokeni çalsa bile işlem yapamaz.
    /// </summary>
    public class Cls_ClaimUserData
    {
        /// <summary>
        /// Tokeni oluşturan kullanıcının ip adresi.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Tokeni oluşturan kullanıcnın browser bilgisi
        /// </summary>
        public string BrowseName { get; set; }

        /// <summary>
        /// Tokenin oluşturulmaz zaman damgası
        /// </summary>
        public double TimeStamp { get; set; }
    }
}
