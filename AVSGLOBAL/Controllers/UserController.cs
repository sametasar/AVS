using Microsoft.AspNetCore.Mvc;
using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Class.GlobalClass;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;


namespace AVSGLOBAL.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUser _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
        
        
        public UserController(IConfiguration config, ITokenService tokenService, IUser userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [Authorize]       
        [HttpGet]        
        /// <summary>
        /// Kullanıcı Listesini getirir.
        /// </summary>     
        public async Task<List<Mdl_User>> Get_Users()
        {
            string UserSession = Cls_Tools.Decrypt(HttpContext.Session.GetString("UserInfo"));     

            Mdl_User User = JsonConvert.DeserializeObject<Mdl_User>(UserSession);

            return await new Cls_User().Get_Users(User);
        }
    }
}
