namespace AVSGLOBAL.Class.Global
{
    /// <summary>
    /// Projenin tüm ayarları buradan yönetilir.Default veritabanı bağlantı bilgileri.Default email hesap bilgileri,token ayar bilgileri.
    /// </summary>
    public static class Cls_Settings
    {
        /// <summary>
        /// Sistemde default olarak kullanılacak ms sql connection string bilgisi.
        /// </summary>
        public static string DefaultConnection { get; set; }
        /// <summary>
        /// Sistemin default olarak mail gönderdiği email adresi.
        /// </summary>
        public static string DefaultEmail { get; set; }

        /// <summary>
        /// Sistemin default olarak eposta gönderen mail adresinin kullanıcı adı bilgisi. Bazı mail hesaplarında farklı olabilmektedir.
        /// </summary>
        public static string DefaultEmailUsername { get; set; }

        /// <summary>
        /// Sistemin default olarak eposta gönderen mail adresinin şifre bilgisi.
        /// </summary>
        public static string DefaultEmailPassword { get; set; }
        /// <summary>
        /// Sistemin default olarak eposta gönderen mail adresinin smtp adresi.
        /// </summary>
        public static string SMTP { get; set; }

        /// <summary>
        /// Sistemin default olarak eposta gönderen mail adresinin port bilgisi.
        /// </summary>
        public static string SMTPPort { get; set; }

        /// <summary>
        /// Default jwt key bilgisi. Jwtnin oluşturacağı tokenı burada belirtilen anahtar ile şifrelenecektir.H256sha formatında şifrelenecek bilgi şifrelenmeden önce byte arraye dönüştürülmelidir.
        /// </summary>
        public static string JWTKEY { get; set; }

        /// <summary>
        ///  Yayıncı hak sahibi, firma bilgisi vb.
        /// </summary>
        /// <value></value>
        public static string JWTISSUER {get; set;}

        /// <summary>
        /// Kapsama alanı
        /// </summary>
        /// <value></value>
        public static string JWTAUDIENCE { get; set; }

        /// <summary>
        /// Bu  client uygulamasının çalıştığı bir web servisi varsa web servisin adresi burada belirtilir. Web servis adresinin sonuna "/" yazmayın!
        /// </summary>
        /// <value></value>
        public static string MAIN_WEB_SERVICE { get; set; }

        /// <summary>
        /// Oluşturulan Jwt tokenin dakika olarak sistemde geçerli olacağı süre.
        /// </summary>
        /// <value></value>
        public static double TokenExpireMinute { get; set; }
    }
}
