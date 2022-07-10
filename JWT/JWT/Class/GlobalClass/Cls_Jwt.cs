using JWT.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using JWT.Class.Dal;
using JWT.Class.Models.GlobalModels;

namespace JWT.Class.GlobalClass
{

    /// <summary>
    /// JWT TOKEN MEKANİZMASINI YÖNETEN CLASS YAPISI, Kullanıcılar bu class da bulunan metotlar ile sisteme otantike olur, login işlemleri burada gerçekleşir.
    /// Login olan kullanıcının roltanımları burada gerçekleşir.
    /// </summary>
    public class Cls_Jwt : IJWT
    {
        DatabaseContext db = new DatabaseContext();

        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// Bu metodun yapılmasının amacı token değerini bir user objesinin Token propertysine veriyorum.
        /// Fakat user objesini taşıma esnasında json a çevirdiğimde içinde bulunan tone karakterleri süslü parantezler ,tek tırnaklar, virgüller , büyük parantezler ve json içinde olabilecek diğer karakterler
        /// Json ı tekrar objeye çevirmemi engelliyor. Bu yüzden özel karakterler ile bu token içinde olabilecek diğer özel karakterleri
        /// değiştiriyorum bu karakterleri tekrar eski haline çevirerek tokenımı doğruluyorum.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string TokenCrypt(string token)
        {
            //json içinde bulunan karakter listesi
            //"{},:'\"[]()"
            //  { = ȶ
            //  } = ȸ
            //  , = Ⱥ
            //  : = Ƚ
            //  ' = Ʉ
            // \" = Ɋ
            //  [ = Ɏ
            //  ] = ɓ
            //  ( = ɗ
            //  ) = ɷ

            token = token.Replace("{", "ȶ").Replace("}", "ȸ").Replace(",", "Ⱥ").Replace(":", "Ƚ").Replace("'", "Ʉ").Replace("\"", "Ɋ").Replace("[", "Ɏ").Replace("]", "ɓ").Replace("(", "ɗ").Replace(")", "ɷ");

            return token;
        }


        /// <summary>
        /// Crypt edilen Tokenimi tekrar eski haline getirmek için kullanıyorum.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string TokenDeCrypt(string token)
        {
            token = token.Replace("ȶ", "{").Replace("ȸ", "}").Replace("Ⱥ", ",").Replace("Ƚ", ":").Replace("Ʉ", "'").Replace("Ɋ", "\"").Replace("Ɏ", "[").Replace("ɓ", "]").Replace("ɗ", "(").Replace("ɷ", ")");

            return token;
        }


        public Cls_Jwt(IConfiguration iConfig)
        {
            Configuration = iConfig;
         
            //Configuration.GetValue<string>("Settings:JWTKEY")
        }

       
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
        public Mdl_User Authhenticate(string Email, string Password, ConnectionInfo Connection)
        {
            #region KULLANICI VARMI


            Mdl_User User = new Mdl_User();

            User = db.User.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault() ?? User; 

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

            Claim ClaimA = new Claim(ClaimTypes.Name, User.Name);
            Claim ClaimB = new Claim(ClaimTypes.Role, "Admin"); //todo Veritabanından gelen rolü burada tanımla! ileride yapılacak.
            Claim ClaimC = new Claim(ClaimTypes.Email, User.Email);

            ClaimListesi.Add(ClaimA);
            ClaimListesi.Add(ClaimB);
            ClaimListesi.Add(ClaimC);
                      
            #region Daha güvenli bir Claim oluşturmak için aşaıdakileri dahil edebilirsin.

            ///Doğrudan tanımlayamadığım özellikleri claime dolaylı olarak tanımlıyorum!
            Cls_ClaimUserData UserData = new Cls_ClaimUserData();

            //Kullanıcı bu sayfa direkt olarak erişseydi bu olacaktı fakat arada başka bir sunucu olduğu için her seferinde o sunucunun ip adresini kayıt edecek ve bir işe yaramayacak.
            //Bu yüzden kullanıcının ip adresini yine kullanıcı bilgilerinin içinden alıyorum. Kullanıcının ıp adresini alma işlemini aradaki service halledecek ve kullanıcı objesine yazacak!
            //Bizde objenin içinden ip adresini alacağız.
            UserData.IPAddress = Connection.RemoteIpAddress.ToString();

            //UserData.IPAddress = User.LastIP;

            UserData.TimeStamp = Cls_Tools.DateTime_To_Timestamp(User.LastLoginTime); //Kullanıcının son giriş yaptığı zaman.  //Kullanıcı direkt olarak bu sunucuya erişmiş olsaydı bu durumda bu kod çalışacaktı.
            //Bizim arada bir service olduğu için login zamanını alma işlemini ona yaptırıyoruz.
            
            //UserData.TimeStamp = Cls_Tools.DateTime_To_Timestamp(User.LastLoginTime); //Kullanıcının son giriş yaptığı zaman.
            UserData.BrowseName = "Mozilla"; //todo kullanıcı browser adı alıanacak!
           
            Claim ClaimD = new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(UserData));
            Claim ClaimE = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());
                     
            ClaimListesi.Add(ClaimD);
            ClaimListesi.Add(ClaimE);

            #endregion

            #endregion

            /*

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

          
           User.LastToken = TokenHandler.WriteToken(Token);

            return User;

            #endregion

            */

            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY));
            SigningCredentials Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken TokenDescriptor = new JwtSecurityToken(Cls_Settings.JWTISSUER, Cls_Settings.JWTISSUER, ClaimListesi,
            expires: DateTime.Now.AddMinutes(Cls_Settings.TokenExpireMinute), signingCredentials: Credentials);
            User.TokenDescriptor = TokenDescriptor;
            User.LastToken = TokenCrypt(new JwtSecurityTokenHandler().WriteToken(TokenDescriptor));
            return User;


        }

        #endregion
    }
}
