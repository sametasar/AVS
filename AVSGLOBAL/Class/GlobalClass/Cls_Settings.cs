namespace AVSGLOBAL.Class.GlobalClass
{

    /* #region  MS SQL */
    /// <summary>
    /// Uygulamanın kullandığı default ms sql erişim bilgileri statik değişkenler ile tüm projede bu class üzerinden erişilebilir.
    /// </summary>
    public static class Cls_DefaultMsSql
    {
        /// <summary>
        /// Kullanıcı adı bilgisi
        /// </summary>
        /// <value></value>
        public static string UserName { get; set; }

        /// <summary>
        /// Şifre bilgisi.
        /// </summary>
        /// <value></value>
        public static string Password { get; set; }

        /// <summary>
        /// Veri kaynağı server ip adresi yada adı.
        /// </summary>
        /// <value></value>
        public static string Server { get; set; }

        /// <summary>
        /// Veritabanının adı.
        /// </summary>
        /// <value></value>
        public static string Database { get; set; }

    }
    /* #endregion */

    /* #region  MY SQL */
    /// <summary>
    /// Uygulamanın kullandığı default my sql erişim bilgileri statik değişkenler ile tüm projede bu class üzerinden erişilebilir.
    /// </summary>
    public static class Cls_DefaultMySql
    {
        /// <summary>
        /// Kullanıcı adı bilgisi
        /// </summary>
        /// <value></value>
        public static string UserName { get; set; }

        /// <summary>
        /// Şifre bilgisi.
        /// </summary>
        /// <value></value>
        public static string Password { get; set; }

        /// <summary>
        /// Veri kaynağı server ip adresi yada adı.
        /// </summary>
        /// <value></value>
        public static string Server { get; set; }

        /// <summary>
        /// Veritabanının adı.
        /// </summary>
        /// <value></value>
        public static string Database { get; set; }

    }

    /* #endregion */

    /* #region  ORACLE */
    /// <summary>
    /// Uygulamanın kullandığı default oracle erişim bilgileri statik değişkenler ile tüm projede bu class üzerinden erişilebilir.
    /// </summary>
    public static class Cls_DefaultOracle
    {
        /// <summary>
        /// Kullanıcı adı bilgisi
        /// </summary>
        /// <value></value>
        public static string UserName { get; set; }

        /// <summary>
        /// Şifre bilgisi.
        /// </summary>
        /// <value></value>
        public static string Password { get; set; }

        /// <summary>
        /// Veri kaynağı server ip adresi yada adı.
        /// </summary>
        /// <value></value>
        public static string Server { get; set; }

        /// <summary>
        /// Veritabanının adı.
        /// </summary>
        /// <value></value>
        public static string Database { get; set; }

    }
    /* #endregion */

    /* #region  POSTGRE SQL */
    /// <summary>
    /// Uygulamanın kullandığı default postgre sql erişim bilgileri statik değişkenler ile tüm projede bu class üzerinden erişilebilir.
    /// </summary>
    public static class Cls_DefaultPostgreSql
    {
        /// <summary>
        /// Kullanıcı adı bilgisi
        /// </summary>
        /// <value></value>
        public static string UserName { get; set; }

        /// <summary>
        /// Şifre bilgisi.
        /// </summary>
        /// <value></value>
        public static string Password { get; set; }

        /// <summary>
        /// Veri kaynağı server ip adresi yada adı.
        /// </summary>
        /// <value></value>
        public static string Server { get; set; }

        /// <summary>
        /// Veritabanının adı.
        /// </summary>
        /// <value></value>
        public static string Database { get; set; }

    }
    /* #endregion */

    /* #region  MONGO DB */
    /// <summary>
    /// Uygulamanın kullandığı default mongo db erişim bilgileri statik değişkenler ile tüm projede bu class üzerinden erişilebilir.
    /// </summary>
    public static class Cls_DefaultMongodb
    {
        /// <summary>
        /// Kullanıcı adı bilgisi
        /// </summary>
        /// <value></value>
        public static string UserName { get; set; }

        /// <summary>
        /// Şifre bilgisi.
        /// </summary>
        /// <value></value>
        public static string Password { get; set; }

        /// <summary>
        /// Veri kaynağı server ip adresi yada adı.
        /// </summary>
        /// <value></value>
        public static string Server { get; set; }

        /// <summary>
        /// Veritabanının adı.
        /// </summary>
        /// <value></value>
        public static string Database { get; set; }

    }
    /* #endregion */

    /* #region  SQL LITE */
    /// <summary>
    /// Uygulamanın kullandığı default sql lite erişim bilgileri statik değişkenler ile tüm projede bu class üzerinden erişilebilir.
    /// </summary>
    public static class Cls_DefaultSqlLite
    {
        /// <summary>
        /// Sqllite dosyasının bulunduğu dizin bilgisi.
        /// </summary>
        /// <value></value>
        public static string Directory { get; set; }

        /// <summary>
        /// Sqllite dosyasının adı.
        /// </summary>
        /// <value></value>
        public static string DataSource { get; set; }

        /// <summary>
        /// Sql lite versiyon bilgisi
        /// </summary>
        /// <value></value>
        public static int Version { get; set; }

        /// <summary>
        /// Sqllite default time out süresi.
        /// </summary>
        /// <value></value>
        public static int DefaultTimeout { get; set; }

        /// <summary>
        /// Değer True olduğunda veritabanı dosyasını bulamazsa bir hata mesajı oluşturur. False olduğunda ise veritabanını bulamazsa yenir tane oluşturur.
        /// </summary>
        /// <value></value>
        public static bool FailIfMissing { get; set; }

        /// <summary>
        /// sqllite dosyası eğer şifreli ise şifre burada tanımlanmalı.
        /// </summary>
        /// <value></value>
        public static string Password { get; set; }

        /// <summary>
        /// Bağlantı readonlu kullanılacaksa true olmalı default false dır.
        /// </summary>
        /// <value></value>
        public static bool Readonly { get; set; }

    }

    /* #endregion */

    /* #region  SETTINGS */
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
    /* #endregion */

}