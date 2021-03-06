using Microsoft.AspNetCore.Mvc;
using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Class.GlobalClass;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace AVSGLOBAL.Controllers
{
    public class LanguageController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUser _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
        public LanguageController(IConfiguration config, ITokenService tokenService, IUser userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Tüm sözlüğü indirir ve cache e alır!
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetLanguageDictionary")]
        [HttpGet]
        public async Task<string> GetLanguageDictionary()
        {
            return await new Cls_Language().GetLanguageDictionary();
        }
    }
}
