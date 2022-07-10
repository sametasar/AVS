using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT.Interface;
using JWT.Class;
using JWT.Class.Models.GlobalModels;
using Newtonsoft.Json;
using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;
using JWT.Class.Dal;

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
         Summary = "Test Kullanýcýsý", Tags = new string[] { "Test Kullanýcýsý" })]       
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

        #endregion      

    }

    #endregion   
}