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
        Cls_DbContext db = new Cls_DbContext();

        private IConfiguration Configuration { get; set; }

        public Cls_Jwt()        {
           
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

            User = db.User.Where(x => x.Email == Email && x.Password == Cls_Tools.EnCrypt(Password)).FirstOrDefault() ?? User; 

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
            Claim ClaimB = new Claim(ClaimTypes.Role, User.IdentityRole);
            Claim ClaimC = new Claim(ClaimTypes.Email, User.Email);

            ClaimListesi.Add(ClaimA);
            ClaimListesi.Add(ClaimB);
            ClaimListesi.Add(ClaimC);
                 
            /*   ŞU AN İÇİN EXTRA AYARLARI İPTAL EDİYORUM TOKEN ÇOK BÜYÜYOR DECRYPT EDERKEN PROBLEMLER YAŞADIM İLERİDE TEKRAR BAKACAM!

            #region Daha güvenli bir Claim oluşturmak için aşaıdakileri dahil edebilirsin.
              
            ///Doğrudan tanımlayamadığım özellikleri claime dolaylı olarak tanımlıyorum!
            Mdl_ClaimUserData UserData = new Mdl_ClaimUserData();

            //Kullanıcı bu sayfa direkt olarak erişseydi bu olacaktı fakat arada başka bir sunucu olduğu için her seferinde o sunucunun ip adresini kayıt edecek ve bir işe yaramayacak.
            //Bu yüzden kullanıcının ip adresini yine kullanıcı bilgilerinin içinden alıyorum. Kullanıcının ıp adresini alma işlemini aradaki service halledecek ve kullanıcı objesine yazacak!
            //Bizde objenin içinden ip adresini alacağız.
            //UserData.IPAddress = Connection.RemoteIpAddress.ToString();

            //todo Kullanıcı ilk login olduğunda eposta,şifre ve ip göndermeli!
            UserData.IPAddress = User.LastIP;

            UserData.TimeStamp = Cls_Tools.DateTime_To_Timestamp(User.LastLoginTime); //Kullanıcının son giriş yaptığı zaman.  //Kullanıcı direkt olarak bu sunucuya erişmiş olsaydı bu durumda bu kod çalışacaktı.
            //Bizim arada bir service olduğu için login zamanını alma işlemini ona yaptırıyoruz.
            
            //UserData.TimeStamp = Cls_Tools.DateTime_To_Timestamp(User.LastLoginTime); //Kullanıcının son giriş yaptığı zaman.
            UserData.BrowseName = "Mozilla"; //todo kullanıcı ilk login için email,password gönderdiğinde browser adıda göndermeli!

            Mdl_Claim ClaimUserInfo = new Mdl_Claim();
            ClaimUserInfo.UserExtraData = UserData;
            ClaimUserInfo.User = User;


            Claim ClaimD = new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(ClaimUserInfo));
            Claim ClaimE = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());
                     
            ClaimListesi.Add(ClaimD);
            ClaimListesi.Add(ClaimE);

            #endregion

            */

            #endregion
                       
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Cls_Settings.JWTKEY));
            SigningCredentials Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken TokenDescriptor = new JwtSecurityToken(Cls_Settings.JWTISSUER, Cls_Settings.JWTISSUER, ClaimListesi,
            expires: DateTime.Now.AddMinutes(Cls_Settings.TokenExpireMinute), signingCredentials: Credentials);
            User.LastServiceToken = new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);
            //Veritabanında bulunan LastServiceToken bilgisini güncellerim.
            db.Update(User);
            db.SaveChanges();
            return User;
        }

        #endregion
    }
}
