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
    [Route("api/[action]")]
    public class TokenController : ControllerBase
    {
        #region PROPERTIES

        /// <summary>
        /// Jwt nin özelliklerini kullanabilmesi için Dependency Injection yöntemi ile bu sýnýfa IJWT özellikleri kazandýrýlýyor.
        /// </summary>
        private IJWT IJwtAuthenticationManager;


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
        public TokenController(ILogger<TokenController> logger, IJWT IJwtAuthenticationManager)
        {
            _logger = logger;
            this.IJwtAuthenticationManager = IJwtAuthenticationManager;

        }
        #endregion       

        #region AUTHENTICATE

        /// <summary>
        /// Sisteme Authenticate Olunabilmesini Salayan Kimlik Doðrulama Metodu.Kullanýcýlar bu servise request yolladýklarýnda bu Authenticate metodunda kimlik doðrulamasýndan geçmeden hiçbir iþlem yapamazlar.
        /// </summary>
        /// <param name="UserName">Tokený talep eden kullanýcý adý.</param>
        /// <param name="Password">Tokeni talep eden kullanýcýnýn þifresi.</param>
        /// <returns>string Token info</returns>
        /// <seealso cref="IJWT"/>
        /// <seealso cref="Cls_Jwt.Authhenticate(string, string, ConnectionInfo)"/>
        [SwaggerOperation(Description = "Bu web apide bulunan ve Authorize iþlemine tutulan metotlarýn çalýþtýrýlabilmesi için bu metot ile login olunmalýdýr.",
        Summary = "Login Ýþlemleri", Tags = new string[] { "AUTHENTICATE" })]
        [AllowAnonymous]
        //[HttpPost("authenticate")]
        [HttpGet]
        //[HttpPost(Name = "authenticate")]
        //public IActionResult Authenticate([FromBody] string UserName, string Password)
        public IActionResult Authenticate(string Email, string Password)
        {
            Mdl_User User = IJwtAuthenticationManager.Authhenticate(Email, Password, HttpContext.Connection);

            if (User == null)
            {
                return Unauthorized();
            }

            return Ok(User);
        }

        /// <summary>
        /// Authenticate i test etmek için geliþtirildi.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Description = "Test amaçlý Authorize Ýþlemi Yapar Ve Geriye Bir Token döndürür!.",
        Summary = "Login Test Ýþlemi", Tags = new string[] { "AUTHENTICATE" })]
        public IActionResult AuthenticateTest()
        {
            Mdl_User User = IJwtAuthenticationManager.Authhenticate("sametasar@gmail.com", "test", HttpContext.Connection);

            if (User == null)
            {
                return Unauthorized();
            }

            return Ok(User.LastServiceToken);
        }
              
        #endregion

    }

    #endregion   
}