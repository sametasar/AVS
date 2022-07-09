using JWT.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;

namespace JWT.Class
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


    /// <summary>
    /// JWT TOKEN MEKANİZMASINI YÖNETEN CLASS YAPISI, Kullanıcılar bu class da bulunan metotlar ile sisteme otantike olur, login işlemleri burada gerçekleşir.
    /// Login olan kullanıcının roltanımları burada gerçekleşir.
    /// </summary>
    public class Cls_Jwt : IJWT
    {
        private IConfiguration Configuration { get; set; }

      

        public Cls_Jwt(IConfiguration iConfig)
        {
            Configuration = iConfig;
         
            //Configuration.GetValue<string>("Settings:JWTKEY")
        }

        #region TEST KULLANICILARI

        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"user1","pwd1" },
            {"user2","pwd2" }
        };

        #endregion

        #region public string Authhenticate(string UserName, string Password)

        /// <summary>
        /// Sisteme giriş yapacak olan kullanıcının sisteme login olabilmesini sağlayacak olan, Jwt Token Mekanizmasına ait kimlik doğrulama yapısıdır.
        /// Bu metot içinde bulunan yapı Jwt teknolojisini Microsoft Identity mekanizmasına uyumlu bir şekilde yapılandırır.
        /// Bu sayede kullanıcı kimlik doğrulama işlemi gerçekleşmiş olacak.Kullanıcı sisteme Authenticate olacak.
        /// Tepesinde [Authorize] yazan sayfalarda bu metotdan yetki alan yani Authenticate olan kullanıcılar giriş yapabilecek.
        /// Sayfalarda [Authorize] dışında ayrıca bir rol tanımı yapıldıysa yine o rol için gerekli kayıt buradan kullanıcıya atanır.
        /// Kullanıcı bilgileri, user yada rol bilgisi kullanıcı claim objesi içinde tanımlanır.
        /// </summary>
        /// <param name="UserName">Kullanıcı Adı</param>
        /// <param name="Password">Kullanıcı Şifresi</param>
        /// <param name="Connection">Kullanıcının bağlantı bilgileri, ip adresi browser vb.</param>
        /// <returns>Token String</returns>
        /// <seealso cref="IJWT"/>
        /// <seealso cref="TokenController.Authenticate"/>
        public string Authhenticate(string UserName, string Password, ConnectionInfo Connection)
        {
            #region KULLANICI VARMI


            if (!users.Any(x=> x.Key == UserName && x.Value == Password))
            {
                return null;
            }

            #endregion
                     
            #region SECRET KEY
            //Jwt Token 3 bölümden oluşur 
            //1- HEADER     (ALGORITHM & TOKEN TYPE bilgileri bulundurur)
            //2- PAYLOAD    (DATA BULUNDURUR)
            //3- VERIFY SIGNATURE (256 BİT SECRET KEY BULUNDURUR)
            //Jwt Tokenda bulunan en alt kısımdaki (VERIFY SIGNATURE) bölümünde bulunan şifre anahtarı. - daha fazla bilgi için https://jwt.io/#debugger-io adresini ziyaret edebilirsin.
            //Jwt Token 3 kısımdan oluşur bu en alt kısımdaki SECRET KEY E KARŞILIK GELİR.
            byte[] TokenKey = Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY);
            #endregion
            
            #region OTANTİKE OLAN KULLANICININ KİMLİK KARTI YAPILANDIRILMASI

            //Claimler otantike olan kullanıcının kimlik bilgisinde bulunacak özellikleri yapılandırıyor.Her eklenen claim aslında sisteme giriş yapan kullanıcı için oluşturulmuş bir özellik.
            //Kimlik yapısını oluşturuyoruz.
            List<Claim> ClaimListesi = new List<Claim>();

            Claim ClaimA = new Claim(ClaimTypes.Name, UserName);

            Claim ClaimB = new Claim(ClaimTypes.Role, "Admin"); //Veritabanından gelen rolü burada tanımla!

            ClaimListesi.Add(ClaimA);
            ClaimListesi.Add(ClaimB);


            #region Daha güvenli bir Claim oluşturmak için aşaıdakileri dahil edebilirsin.

            Cls_ClaimUserData UserData = new Cls_ClaimUserData();
            
            UserData.IPAddress = Connection.RemoteIpAddress.ToString();
            UserData.BrowseName = "Mozilla";
            UserData.TimeStamp = Cls_Tools.DateTime_To_Timestamp(DateTime.Now);

            Claim IP = new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(UserData)); //Veritabanından gelen rolü burada tanımla!

            #endregion


            #endregion

            #region TOKEN ÖZELLİKLERİ

            //Tokenimizi handle ederken, Token yaratırken bazı parametreler gerekli.
            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor();

            //Claim Identity İster
            TokenDescriptor.Subject = new ClaimsIdentity(ClaimListesi);

            //Datetim.Now yerine Datetime.UtcNow kullanmak uluslar arası projelerde daha doğru olacaktır.
            TokenDescriptor.Expires = DateTime.UtcNow.AddHours(1);

            

            SigningCredentials Credentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature);
            TokenDescriptor.SigningCredentials = Credentials;

            #endregion

            #region TOKENİ OLUŞTURMA

            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

            SecurityToken Token = TokenHandler.CreateToken(TokenDescriptor);

            return TokenHandler.WriteToken(Token);

            #endregion
        }

        #endregion
    }
}
