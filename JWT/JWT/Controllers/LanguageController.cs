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
    [Authorize]
    [ApiController]
    [Route("language/[action]")]
    public class LanguageController : ControllerBase
    {
        Cls_DbContext db = new Cls_DbContext();

        #region GetLanguageDictionary

        /// <summary>
        /// Çoklu dil için geliştirildi. Bu uygulamada kullanılan tüm sözlük bilgisini döndürür.
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation(Description = "Çoklu dil için geliştirildi. Bu uygulamada kullanılan tüm sözlük bilgisini döndürür.",
         Summary = "Çoklu dil sözlük", Tags = new string[] { "Tüm dillere ait kelimeler." })]
        [HttpPost]
        [Description("Çoklu dil için geliştirildi. Bu uygulamada kullanılan tüm sözlük bilgisini döndürür.")]
        public string GetLanguageDictionary(dynamic UserInfo)
        {
            string UserJson = Cls_Tools.Decrypt(UserInfo);

            Mdl_User User = JsonConvert.DeserializeObject<Mdl_User>(UserInfo);

            return "deneme";
        }

        #endregion
    }
}
