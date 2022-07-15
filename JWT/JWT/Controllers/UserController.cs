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
    /// Bu s�n�f Authenticate ve Authorize i�lemlerinin yap�labilmesi i�in olu�turulmu�tur.Kullan�c�lar bu s�n�fda i�lem yapmadan �nce sisteme Authenticate olmalar� gerekmektedir.
    /// Authenticate metodu Anonymus olarak olu�turulduu i�in Authenticate olamdan �al���r.Fakat di�er metotlar Authenticate olmadan �al��maz.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("user/[action]")]
    public class UserController : ControllerBase
    {
      
        #region PROPERTIES       

        /// <summary>
        /// Loglama i�lemleri i�in gerekli olan parametreleri i�inde bulunduran de�i�kendir.
        /// </summary>
        private readonly ILogger<TokenController> _logger;

        #endregion

        #region KURUCU        

        /// <summary>
        /// TokenController i�in olu�turulan s�n�f kurucusu. Parametreden loglama ve Authenticate i�lemleri i�in gerekli objeler gelmektedir.
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

        [SwaggerOperation(Description = "Test Kullan�c�s bilgilerini d�nd�r�r.",
         Summary = "Test Kullan�c�s� Getirir", Tags = new string[] { "Test Kullan�c�s� Getirir" })]       
        [HttpGet]
        [Description("Yaln�zca test kullan�c�s� bilgilerini d�nd�r�r.")]
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

        [SwaggerOperation(Description = "Uygulama Default Key ile �ifrelenen kelimenin �ifresini ��zer.",
         Summary = "Default Password Decrypt", Tags = new string[] { "Default Password Decrypt" })]
        [HttpGet]
        [Description("Uygulama Default Key ile �ifrelenen kelimenin �ifresini ��zer.")]
        public string StandartDecrypt(string Text)
        {
            return Cls_Tools.Decrypt(Text);
        }

        #endregion

        #region CUSTOM PASSWORD DECRYPT

        [SwaggerOperation(Description = "Kullan�c�n�n belirledi�i Key  ile �ifrelenen kelimenin �ifresini ��zer.",
         Summary = "Default Password Decrypt", Tags = new string[] { "Default Password Decrypt" })]
        [HttpGet]
        [Description("Kullan�c�n�n belirledi�i Key  ile �ifrelenen kelimenin �ifresini ��zer.")]
        public string CustomDecrypt(string Text,string Key)
        {
            return Cls_Tools.Decrypt(Text,Key);
        }

        #endregion

        #region STANDART PASSWORD ENCRYPT

        [SwaggerOperation(Description = "Uygulama Default Keyi ile text �ifreler.",
         Summary = "Default Password Encrypt", Tags = new string[] { "Default Password Encrypt" })]
        [HttpGet]
        [Description("Uygulama Default Keyi ile text �ifreler.")]
        public string StandartEncrypt(string Text)
        {
            return Cls_Tools.EnCrypt(Text);
        }

        #endregion

        #region CUSTOM PASSWORD ENCRYPT

        [SwaggerOperation(Description = "Kullan�c�n�n belirledi�i Key ile verilen texti �ifreler.",
         Summary = "Custom Password Encrypt", Tags = new string[] { "Custom Password Encrypt" })]
        [HttpGet]
        [Description("Kullan�c�n�n belirledi�i Key ile verilen texti �ifreler.")]
        public string CustomEncrypt(string Text, string Key)
        {
            return Cls_Tools.EnCrypt(Text, Key);
        }

        #endregion

        #region UPDATE_CLIENT_TOKEN_&_IP

        /// <summary>
        /// Kullan�c�n�n Client taraf�nda bulunan token ve IP bilgisini veritaban�na bulunan kolonlar�n� g�nceller. Her zaman kullan�c�n�n en son ip adresi ve token bilgisi User tablosunda ilgili sat�r�nda g�ncel kal�r.
        /// Kullan�c�n�n veritaban�nad g�ncellenen bu alanlar�n�n isimleri, [LastToken] ve [LastIP] alanlar�d�r.
        /// </summary>
        [SwaggerOperation(Description = "Bir kullan�c�n�n client tarf�nda olu�turdu�u token bilgisini veritaban�na yazar.",
         Summary = "Client Token Update Database", Tags = new string[] { "Client Token Update Database" })]
        [HttpPost]
        [Description("Bir kullan�c�n�n client tarf�nda olu�turdu�u token bilgisini veritaban�na yazar.")]
        public async Task<string> UpdateClientTokenAndIP(List<Mdl_KeyValue> data)
        {            
            if(data.Count<3)
            {
                return "Hata! Parametrelerin parse edilemiyor!";
            }
            
            string UserID = data[0].Value;
            string ClientToken= data[1].Value;
            string LastIP= data[2].Value;

            //Kullan�c�y� getir g�ncelle veritaban�na yaz!

            DatabaseContext db = new DatabaseContext();

            Mdl_User User = db.User.Where(x => x.ID == Convert.ToInt32(UserID)).FirstOrDefault();

            User.LastToken = ClientToken;
            User.LastIP = LastIP; //Client�n IP adresi kay�t edilir.

            db.User.Update(User);

            return (await db.SaveChangesAsync()).ToString();
        }

        #endregion

        #endregion

    }
    #endregion
}