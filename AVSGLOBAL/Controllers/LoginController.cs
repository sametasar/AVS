using Microsoft.AspNetCore.Mvc;
using AVSGLOBAL.Class.Models;
using AVSGLOBAL.Class.Global;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authorization;


namespace AVSGLOBAL.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUser _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
    public LoginController(IConfiguration config, ITokenService tokenService, IUser userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public IActionResult LoginPageVue()
        {
            return View();
        }

        public IActionResult LoginPageReact()
        {
            return View();
        }

        private async Task<Mdl_User> GetUser(Mdl_User userModel)
        {
            //Write your code here to authenticate the user
            return await _userRepository.GetUser(userModel);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpGet]
        public async Task<IActionResult> Login(string UserName,string Password)
        {

            Mdl_User userModel = new Mdl_User();
            userModel.Name = UserName;
            userModel.Password = Password;


            if (string.IsNullOrEmpty(userModel.Name) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            IActionResult response = Unauthorized();
            Mdl_User validUser = await GetUser(userModel);

            if (validUser != null)
            {
                generatedToken = _tokenService.BuildToken(Cls_Settings.JWTKEY, Cls_Settings.JWTISSUER, validUser, HttpContext.Connection);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return RedirectToAction("MainWindow");
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }        
    }
}
