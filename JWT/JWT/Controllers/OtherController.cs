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
    public class OtherController : ControllerBase
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
        public OtherController(ILogger<TokenController> logger)
        {
            _logger = logger;           
        }
        #endregion

        #region METHODS

        #region HAVA DURUMU

        [SwaggerOperation(Description = "Bulundu�unuz lokasyondaki 1 haftal�k hava durumu bilgilerini getirir.", Summary = "Hava Durumu", Tags = new string[] { "Hava Durumu" })]
        [Authorize]
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
        [Authorize]
        [HttpGet]
        [Description("T�rkiye cumhuriyeti merkez bankas�ndan en g�ncel d�viz kur bilgilerini getirir.")]
        public List<Mdl_Currency> GetDataTableAllCurrenciesTodaysExchangeRates()
        {
             
            return CurrenciesExchange.GetDataTableAllCurrenciesTodaysExchangeRates();
        }

        #endregion      

        #endregion      

    }

    #endregion   
}