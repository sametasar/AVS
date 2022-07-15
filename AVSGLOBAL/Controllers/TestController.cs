﻿using Microsoft.AspNetCore.Mvc;
using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Class.GlobalClass;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AVSGLOBAL.Class.Dal;


namespace AVSGLOBAL.Controllers
{
    public class TestController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUser _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
        public TestController(IConfiguration config, ITokenService tokenService, IUser userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        #region CREATE TEST DATA

        [AllowAnonymous]       
        [HttpGet]        
        /// <summary>
        /// Test Dataları Oluşturur.
        /// </summary>     
        public async void Create_TestData()
        {
            DatabaseContext db = new DatabaseContext();

            #region DİLLER OLUŞTURULUR

            Mdl_Language Language1 = new Mdl_Language();
            Language1.Name = "Türkçe";
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

            #region DİLLERE KELİMELER EKLENİR

            Mdl_LanguageDictionary Ld1 = new Mdl_LanguageDictionary();
            Ld1.LanguageID = 1;
            Ld1.ControlID = 1;
            Ld1.Word = "Başlık";

            Mdl_LanguageDictionary Ld2 = new Mdl_LanguageDictionary();
            Ld2.LanguageID = 1;
            Ld2.ControlID = 2;
            Ld2.Word = "Giriş";

            Mdl_LanguageDictionary Ld3 = new Mdl_LanguageDictionary();
            Ld3.LanguageID = 1;
            Ld3.ControlID = 3;
            Ld3.Word = "Kullanıcı Adı";

            Mdl_LanguageDictionary Ld4 = new Mdl_LanguageDictionary();
            Ld4.LanguageID = 1;
            Ld4.ControlID = 4;
            Ld4.Word = "Şifremi Unuttum";

            Mdl_LanguageDictionary Ld5 = new Mdl_LanguageDictionary();
            Ld5.LanguageID = 1;
            Ld5.ControlID = 5;
            Ld5.Word = "Beni Hatırla";

            Mdl_LanguageDictionary Ld6 = new Mdl_LanguageDictionary();
            Ld6.LanguageID = 1;
            Ld6.ControlID = 6;
            Ld6.Word = "Kayıt Ol";

            Mdl_LanguageDictionary Ld7 = new Mdl_LanguageDictionary();
            Ld7.LanguageID = 1;
            Ld7.ControlID = 7;
            Ld7.Word = "E Posta";

            Mdl_LanguageDictionary Ld8 = new Mdl_LanguageDictionary();
            Ld8.LanguageID = 1;
            Ld8.ControlID = 8;
            Ld8.Word = "Şifre";

            Mdl_LanguageDictionary Ld9 = new Mdl_LanguageDictionary();
            Ld9.LanguageID = 1;
            Ld9.ControlID = 9;
            Ld9.Word = "Ekranı";

            db.LanguageDictionary.Add(Ld1);
            db.LanguageDictionary.Add(Ld2);
            db.LanguageDictionary.Add(Ld3);
            db.LanguageDictionary.Add(Ld4);
            db.LanguageDictionary.Add(Ld5);
            db.LanguageDictionary.Add(Ld6);
            db.LanguageDictionary.Add(Ld7);
            db.LanguageDictionary.Add(Ld8);
            db.LanguageDictionary.Add(Ld9);


            //İNGİLİZCE

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
            Lm4.Word = "Forgot Password";

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

            Mdl_LanguageDictionary Lm9 = new Mdl_LanguageDictionary();
            Lm9.LanguageID = 2;
            Lm9.ControlID = 9;
            Lm9.Word = "Screen";

            db.LanguageDictionary.Add(Lm1);
            db.LanguageDictionary.Add(Lm2);
            db.LanguageDictionary.Add(Lm3);
            db.LanguageDictionary.Add(Lm4);
            db.LanguageDictionary.Add(Lm5);
            db.LanguageDictionary.Add(Lm6);
            db.LanguageDictionary.Add(Lm7);
            db.LanguageDictionary.Add(Lm8);
            db.LanguageDictionary.Add(Lm9);

            #endregion

            #region KULLANICILAR OLUŞTURULUR

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
    }
}
