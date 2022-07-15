using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT.Interface;
using JWT.Class;
using JWT.Class.Models.GlobalModels;
using Newtonsoft.Json;
using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;
using JWT.Class.Dal;
using JWT.Class.GlobalClass;

namespace JWT.Controllers
{
    #region TokenController

    /// <summary>
    /// Bu sýnýf Authenticate ve Authorize iþlemlerinin yapýlabilmesi için oluþturulmuþtur.Kullanýcýlar bu sýnýfda iþlem yapmadan önce sisteme Authenticate olmalarý gerekmektedir.
    /// Authenticate metodu Anonymus olarak oluþturulduu için Authenticate olamdan çalýþýr.Fakat diðer metotlar Authenticate olmadan çalýþmaz.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("user/[action]")]
    public class UserController : ControllerBase
    {
      
        #region PROPERTIES       

        /// <summary>
        /// Loglama iþlemleri için gerekli olan parametreleri içinde bulunduran deðiþkendir.
        /// </summary>
        private readonly ILogger<TokenController> _logger;

        #endregion

        #region KURUCU        

        /// <summary>
        /// TokenController için oluþturulan sýnýf kurucusu. Parametreden loglama ve Authenticate iþlemleri için gerekli objeler gelmektedir.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="IJwtAuthenticationManager"></param>
        public UserController(ILogger<TokenController> logger, IJWT IJwtAuthenticationManager)
        {
            _logger = logger;           
        }
        #endregion

        #region METHODS       

        #region GET USER

        [SwaggerOperation(Description = "Test Kullanýcýs bilgilerini döndürür.",
         Summary = "Test Kullanýcýsý Getirir", Tags = new string[] { "Test Kullanýcýsý Getirir" })]       
        [HttpGet]
        [Description("Yalnýzca test kullanýcýsý bilgilerini döndürür.")]
        public Mdl_User Get_Users()
        {
            Mdl_User Kisi = new Mdl_User();
            Kisi.Name = "Mesut";
            Kisi.Password = "1234";
            Kisi.Email = "mesuthas@hotmail.com";
            Kisi.Surname = "Has";
            return Kisi;
        }

        #endregion
    
        #region STANDART PASSWORD DECRYPT

        [SwaggerOperation(Description = "Uygulama Default Key ile þifrelenen kelimenin þifresini çözer.",
         Summary = "Default Password Decrypt", Tags = new string[] { "Default Password Decrypt" })]
        [HttpGet]
        [Description("Uygulama Default Key ile þifrelenen kelimenin þifresini çözer.")]
        public string StandartDecrypt(string Text)
        {
            return Cls_Tools.Decrypt(Text);
        }

        #endregion

        #region CUSTOM PASSWORD DECRYPT

        [SwaggerOperation(Description = "Kullanýcýnýn belirlediði Key  ile þifrelenen kelimenin þifresini çözer.",
         Summary = "Default Password Decrypt", Tags = new string[] { "Default Password Decrypt" })]
        [HttpGet]
        [Description("Kullanýcýnýn belirlediði Key  ile þifrelenen kelimenin þifresini çözer.")]
        public string CustomDecrypt(string Text,string Key)
        {
            return Cls_Tools.Decrypt(Text,Key);
        }

        #endregion

        #region STANDART PASSWORD ENCRYPT

        [SwaggerOperation(Description = "Uygulama Default Keyi ile text þifreler.",
         Summary = "Default Password Encrypt", Tags = new string[] { "Default Password Encrypt" })]
        [HttpGet]
        [Description("Uygulama Default Keyi ile text þifreler.")]
        public string StandartEncrypt(string Text)
        {
            return Cls_Tools.EnCrypt(Text);
        }

        #endregion

        #region CUSTOM PASSWORD ENCRYPT

        [SwaggerOperation(Description = "Kullanýcýnýn belirlediði Key ile verilen texti þifreler.",
         Summary = "Custom Password Encrypt", Tags = new string[] { "Custom Password Encrypt" })]
        [HttpGet]
        [Description("Kullanýcýnýn belirlediði Key ile verilen texti þifreler.")]
        public string CustomEncrypt(string Text, string Key)
        {
            return Cls_Tools.EnCrypt(Text, Key);
        }

        #endregion

        #region UPDATE_CLIENT_TOKEN_&_IP

        /// <summary>
        /// Kullanýcýnýn Client tarafýnda bulunan token ve IP bilgisini veritabanýna bulunan kolonlarýný günceller. Her zaman kullanýcýnýn en son ip adresi ve token bilgisi User tablosunda ilgili satýrýnda güncel kalýr.
        /// Kullanýcýnýn veritabanýnad güncellenen bu alanlarýnýn isimleri, [LastToken] ve [LastIP] alanlarýdýr.
        /// </summary>
        [SwaggerOperation(Description = "Bir kullanýcýnýn client tarfýnda oluþturduðu token bilgisini veritabanýna yazar.",
         Summary = "Client Token Update Database", Tags = new string[] { "Client Token Update Database" })]
        [HttpPost]
        [Description("Bir kullanýcýnýn client tarfýnda oluþturduðu token bilgisini veritabanýna yazar.")]
        public async Task<string> UpdateClientTokenAndIP(List<Mdl_KeyValue> data)
        {            
            if(data.Count<3)
            {
                return "Hata! Parametrelerin parse edilemiyor!";
            }
            
            string UserID = data[0].Value;
            string ClientToken= data[1].Value;
            string LastIP= data[2].Value;

            //Kullanýcýyý getir güncelle veritabanýna yaz!

            DatabaseContext db = new DatabaseContext();

            Mdl_User User = db.User.Where(x => x.ID == Convert.ToInt32(UserID)).FirstOrDefault();

            User.LastToken = ClientToken;
            User.LastIP = LastIP; //Clientýn IP adresi kayýt edilir.

            db.User.Update(User);

            return (await db.SaveChangesAsync()).ToString();
        }

        #endregion

        #endregion

    }
    #endregion
}