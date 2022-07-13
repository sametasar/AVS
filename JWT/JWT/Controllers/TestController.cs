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
    [Route("other/[action]")]
    public class TestController : ControllerBase
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
        public TestController(ILogger<TokenController> logger)
        {
            _logger = logger;           
        }
        #endregion

        #region METHODS      

        #region CREATE TEST DATA

        [AllowAnonymous]
        [SwaggerOperation(Description = "Test Datalar� Olu�turur.",
         Summary = "Test Datalar� Olu�tur", Tags = new string[] { "Test Datalar� Olu�turur" })]
        [HttpGet]
        [Description("Test Datalar� Olu�tur.")]
        /// <summary>
        /// Test Datalar� Olu�turur.
        /// </summary>     
        public async void Create_TestData()
        {
            Cls_DbContext db = new Cls_DbContext();

            #region D�LLER OLU�TURULUR

            Mdl_Language Language1 = new Mdl_Language();
            Language1.Name = "T�rk�e";
            Language1.Default = true;
            Language1.Picture = "img/Bayrak1.jpg";

            Mdl_Language Language2 = new Mdl_Language();
            Language2.Name = "English";
            Language2.Default = false;
            Language2.Picture = "img/Bayrak2.jpg";

            Mdl_Language Language3 = new Mdl_Language();
            Language3.Name = "Germany";
            Language3.Default = false;
            Language3.Picture = "img/Bayrak3.jpg";

            db.Language.Add(Language1);
            db.Language.Add(Language2);
            db.Language.Add(Language3);

            #endregion

            #region D�LLERE KEL�MELER EKLEN�R

            Mdl_LanguageDictionary Ld1 = new Mdl_LanguageDictionary();
            Ld1.LanguageID = 1;
            Ld1.ControlID = 1;
            Ld1.Word = "Ba�l�k";

            Mdl_LanguageDictionary Ld2 = new Mdl_LanguageDictionary();
            Ld2.LanguageID = 1;
            Ld2.ControlID = 2;
            Ld2.Word = "Giri�";

            Mdl_LanguageDictionary Ld3 = new Mdl_LanguageDictionary();
            Ld3.LanguageID = 1;
            Ld3.ControlID = 3;
            Ld3.Word = "Kullan�c� Ad�";

            Mdl_LanguageDictionary Ld4 = new Mdl_LanguageDictionary();
            Ld4.LanguageID = 1;
            Ld4.ControlID = 4;
            Ld4.Word = "�ifremi Unuttum";

            Mdl_LanguageDictionary Ld5 = new Mdl_LanguageDictionary();
            Ld5.LanguageID = 1;
            Ld5.ControlID = 5;
            Ld5.Word = "Beni Hat�rla";

            Mdl_LanguageDictionary Ld6 = new Mdl_LanguageDictionary();
            Ld6.LanguageID = 1;
            Ld6.ControlID = 6;
            Ld6.Word = "Kay�t Ol";

            Mdl_LanguageDictionary Ld7 = new Mdl_LanguageDictionary();
            Ld7.LanguageID = 1;
            Ld7.ControlID = 7;
            Ld7.Word = "E Posta";

            Mdl_LanguageDictionary Ld8 = new Mdl_LanguageDictionary();
            Ld8.LanguageID = 1;
            Ld8.ControlID = 8;
            Ld8.Word = "�ifre";

            db.LanguageDictionary.Add(Ld1);
            db.LanguageDictionary.Add(Ld2);
            db.LanguageDictionary.Add(Ld3);
            db.LanguageDictionary.Add(Ld4);
            db.LanguageDictionary.Add(Ld5);
            db.LanguageDictionary.Add(Ld6);
            db.LanguageDictionary.Add(Ld7);
            db.LanguageDictionary.Add(Ld8);


            //�NG�L�ZCE

            Mdl_LanguageDictionary Lm1 = new Mdl_LanguageDictionary();
            Lm1.LanguageID = 2;
            Lm1.ControlID = 1;
            Lm1.Word = "Title";

            Mdl_LanguageDictionary Lm2 = new Mdl_LanguageDictionary();
            Lm2.LanguageID = 2;
            Lm2.ControlID = 2;
            Lm2.Word = "Login";

            Mdl_LanguageDictionary Lm3 = new Mdl_LanguageDictionary();
            Lm3.LanguageID = 2;
            Lm3.ControlID = 3;
            Lm3.Word = "User Name";

            Mdl_LanguageDictionary Lm4 = new Mdl_LanguageDictionary();
            Lm4.LanguageID = 2;
            Lm4.ControlID = 4;
            Lm4.Word = "Forgot PAssword";

            Mdl_LanguageDictionary Lm5 = new Mdl_LanguageDictionary();
            Lm5.LanguageID = 2;
            Lm5.ControlID = 5;
            Lm5.Word = "Remember Me";

            Mdl_LanguageDictionary Lm6 = new Mdl_LanguageDictionary();
            Lm6.LanguageID = 2;
            Lm6.ControlID = 6;
            Lm6.Word = "Register";

            Mdl_LanguageDictionary Lm7 = new Mdl_LanguageDictionary();
            Lm7.LanguageID = 2;
            Lm7.ControlID = 7;
            Lm7.Word = "Email";

            Mdl_LanguageDictionary Lm8 = new Mdl_LanguageDictionary();
            Lm8.LanguageID = 2;
            Lm8.ControlID = 8;
            Lm8.Word = "Password";

            db.LanguageDictionary.Add(Lm1);
            db.LanguageDictionary.Add(Lm2);
            db.LanguageDictionary.Add(Lm3);
            db.LanguageDictionary.Add(Lm4);
            db.LanguageDictionary.Add(Lm5);
            db.LanguageDictionary.Add(Lm6);
            db.LanguageDictionary.Add(Lm7);
            db.LanguageDictionary.Add(Lm8);

            #endregion

            #region KULLANICILAR OLU�TURULUR

            Mdl_User Kisi = new Mdl_User();
            Kisi.Name = "Samet";
            Kisi.Surname = "Asar";
            Kisi.Password = Cls_Tools.EnCrypt("test");
            Kisi.Email = "sametasar@gmail.com";
            Kisi.Phone = "905368372837";
            Kisi.Create_Date = DateTime.Now;
            Kisi.UserID = 1;
            Kisi.LastToken = "1111121213123213213";
            Kisi.LastServiceToken = "222232434232432432432432";
            Kisi.IdentityRole = "admin";
            Kisi.LastIP = "127.0.0.1";
            Kisi.LastLoginTime = DateTime.Now;
            Kisi.LanguageID = 1;

            db.User.Add(Kisi);


            Mdl_User Kisi2 = new Mdl_User();
            Kisi2.Name = "Mesut";
            Kisi2.Surname = "Has";
            Kisi2.Password = Cls_Tools.EnCrypt("1234");
            Kisi2.Email = "mesuthas@gmail.com";
            Kisi2.Phone = "905352111225";
            Kisi2.Create_Date = DateTime.Now;
            Kisi2.UserID = 1;
            Kisi2.LastToken = "333445345435435345345";
            Kisi2.LastServiceToken = "444546456456456456456";
            Kisi2.IdentityRole = "accountant";
            Kisi2.LastIP = "127.0.0.1";
            Kisi2.LastLoginTime = DateTime.Now;
            Kisi2.LanguageID = 2;

            db.User.Add(Kisi2);

            #endregion

            await db.SaveChangesAsync();

            db.Dispose();
        }

        #endregion

        #endregion      

    }

    #endregion   
}