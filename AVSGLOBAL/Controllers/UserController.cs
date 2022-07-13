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


        
    }
}
