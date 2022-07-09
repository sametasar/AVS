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
    /// Bu s�n�f Authenticate ve Authorize i�lemlerinin yap�labilmesi i�in olu�turulmu�tur.Kullan�c�lar bu s�n�fda i�lem yapmadan �nce sisteme Authenticate olmalar� gerekmektedir.
    /// Authenticate metodu Anonymus olarak olu�turulduu i�in Authenticate olamdan �al���r.Fakat di�er metotlar Authenticate olmadan �al��maz.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[action]")]
    public class TokenController : ControllerBase
    {
        #region PROPERTIES

        /// <summary>
        /// Jwt nin �zelliklerini kullanabilmesi i�in Dependency Injection y�ntemi ile bu s�n�fa IJWT �zellikleri kazand�r�l�yor.
        /// </summary>
        private IJWT IJwtAuthenticationManager;

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
        public TokenController(ILogger<TokenController> logger, IJWT IJwtAuthenticationManager)
        {
            _logger = logger;
            this.IJwtAuthenticationManager = IJwtAuthenticationManager;
        }
        #endregion

        #region METHODS

        #region HAVA DURUMU

        [SwaggerOperation(Description = "Bulundu�unuz lokasyondaki 1 haftal�k hava durumu bilgilerini getirir.", Summary = "Hava Durumu", Tags = new string[] { "Hava Durumu" })]
        [AllowAnonymous]
        [HttpGet]
        [Description("Bulundu�unuz lokasyondaki 1 haftal�k hava durumu bilgilerini getirir.")]
        public Mdl_WeatherObject GetWeather(string city)
        {
            Mdl_WeatherObject Weather = new Mdl_WeatherObject();
            Weather = Weather.GetWeather(city);            
            return Weather;
        }

        #endregion

        #region D�V�Z KURLARI

      
        [SwaggerOperation(Description = "T�rkiye cumhuriyeti merkez bankas�ndan en g�ncel d�viz kur bilgilerini getirir.",
         Summary = "D�viz Kurlar�", Tags = new string[] { "D�viz Kurlar�" })] 
        [AllowAnonymous]
        [HttpGet]
        [Description("T�rkiye cumhuriyeti merkez bankas�ndan en g�ncel d�viz kur bilgilerini getirir.")]
        public List<Mdl_Currency> GetDataTableAllCurrenciesTodaysExchangeRates()
        {
             
            return CurrenciesExchange.GetDataTableAllCurrenciesTodaysExchangeRates();
        }

        #endregion

        #endregion

        #region AUTHENTICATE

        /// <summary>
        /// Sisteme Authenticate Olunabilmesini Salayan Kimlik Do�rulama Metodu.Kullan�c�lar bu servise request yollad�klar�nda bu Authenticate metodunda kimlik do�rulamas�ndan ge�meden hi�bir i�lem yapamazlar.
        /// </summary>
        /// <param name="UserName">Token� talep eden kullan�c� ad�.</param>
        /// <param name="Password">Tokeni talep eden kullan�c�n�n �ifresi.</param>
        /// <returns>string Token info</returns>
        /// <seealso cref="IJWT"/>
        /// <seealso cref="Cls_Jwt.Authhenticate(string, string, ConnectionInfo)"/>
        [SwaggerOperation(Description = "Bu web apide bulunan ve Authorize i�lemine tutulan metotlar�n �al��t�r�labilmesi i�in bu metot ile login olunmal�d�r.",
        Summary = "Login ��lemleri",Tags =new string[] { "Login" })]
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