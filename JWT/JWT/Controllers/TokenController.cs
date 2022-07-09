using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT.Interface;
using JWT.Class;
using JWT.Model;
using Newtonsoft.Json;
using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

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

        #region METHODS

        #region HAVA DURUMU

        [SwaggerOperation(Description = "Bulunduðunuz lokasyondaki 1 haftalýk hava durumu bilgilerini getirir.", Summary = "Hava Durumu", Tags = new string[] { "Hava Durumu" })]
        [AllowAnonymous]
        [HttpGet]
        [Description("Bulunduðunuz lokasyondaki 1 haftalýk hava durumu bilgilerini getirir.")]
        public Mdl_WeatherObject GetWeather(string city)
        {
            Mdl_WeatherObject Weather = new Mdl_WeatherObject();
            Weather = Weather.GetWeather(city);            
            return Weather;
        }

        #endregion

        #region DÖVÝZ KURLARI

      
        [SwaggerOperation(Description = "Türkiye cumhuriyeti merkez bankasýndan en güncel döviz kur bilgilerini getirir.",
         Summary = "Döviz Kurlarý", Tags = new string[] { "Döviz Kurlarý" })] 
        [AllowAnonymous]
        [HttpGet]
        [Description("Türkiye cumhuriyeti merkez bankasýndan en güncel döviz kur bilgilerini getirir.")]
        public List<Mdl_Currency> GetDataTableAllCurrenciesTodaysExchangeRates()
        {
             
            return CurrenciesExchange.GetDataTableAllCurrenciesTodaysExchangeRates();
        }

        #endregion

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
        Summary = "Login Ýþlemleri",Tags =new string[] { "Login" })]
        [AllowAnonymous]
        //[HttpPost("authenticate")]
        [HttpGet]
        //[HttpPost(Name = "authenticate")]
        //public IActionResult Authenticate([FromBody] string UserName, string Password)
        public IActionResult Authenticate(string UserName, string Password)
        {          

            string token = IJwtAuthenticationManager.Authhenticate(UserName, Password, HttpContext.Connection);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        #endregion

    }

    #endregion   
}