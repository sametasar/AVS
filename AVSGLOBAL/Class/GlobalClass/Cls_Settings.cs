namespace AVSGLOBAL.Class.GlobalClass
{

     public static class Cls_DefaultMsSql
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Server { get; set; }

        public static string Database { get; set; }

    }

    public static class Cls_DefaultMySql
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Server { get; set; }

        public static string Database { get; set; }

    }


    public static class Cls_DefaultOracle
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Server { get; set; }

        public static string Database { get; set; }

    }

    public static class Cls_DefaultPostgreSql
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Server { get; set; }

        public static string Database { get; set; }

    }


    public static class Cls_DefaultMongodb
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Server { get; set; }

        public static string Database { get; set; }

    }

  
    public static class Cls_DefaultSqlLite
    {
        public static string Directory { get; set; }

        public static string DataSource { get; set; }

        public static int Version { get; set; }

        public static int DefaultTimeout { get; set; }

        public static bool FailIfMissing { get; set; }

        public static string Password { get; set; }

        public static bool Readonly { get; set; }

    }


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
        /// Yayıncı hak sahibi, firma bilgisi vb.
        /// </summary>
        public static string JWTISSUER { get; set; }

        /// <summary>
        /// Kapsama alanı
        /// </summary>
        public static string JWTAUDIENCE { get; set; }
       
        /// <summary>
        /// Oluşturulan Jwt tokenin dakika olarak sistemde geçerli olacağı süre.
        /// </summary>
        public static double TokenExpireMinute { get; set; }

        /// <summary>
        /// Uygulama içersinde string bilgileri şifrelemede kullanılır. Bazen veritabanına şifre kayıt ederken kvkk ya uygun data şifreleme için, bazende cookir oluştururken cookie içinde alıacak bilgileri decrypt ederken.
        /// Bu tür şifrelemeleri yaparken default olarak kullanılmaktadır. Bu bilginin veritabanı şifrelemelerinde sürekli değiştirilmemesi gereklidir. Önceden şifrelenen  veri tabanında bulunan şifreler bu değiştiğinde çözülemez!
        /// Tamamen değiştirilecekse önce veri tabanında kli şifreler decrype edilmeli ve sonrasında tekrar yeni şifre ile şifrelenerek yerine konmalıdır.
        /// </summary>
        public static string DefaultPasswordKey { get; set; }

        /// <summary>
        /// Uygulamanın default olarak kullandığı veritabanı. MS SQL , MYSQL, ORACLE, POSTGRE,MONGO DB VB.
        /// </summary>
        public static string SelectDatabseEngine { get; set; }

        /// <summary>
        /// Web servis adresi
        /// </summary>
        /// <value></value>
        public static string MAIN_WEB_SERVICE { get; set; }
    } 
 
}
