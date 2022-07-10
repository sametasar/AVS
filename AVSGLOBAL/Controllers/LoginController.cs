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
       
        [AllowAnonymous]
        [Route("login")]
        [HttpGet]
        public async Task<IActionResult> Login(string Email,string Password)
        {

            Mdl_User userModel = new Mdl_User();
            userModel.Email = Email;
            userModel.Password = Password;


            if (string.IsNullOrEmpty(userModel.Email) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            IActionResult response = Unauthorized();
            Mdl_User validUser = await _userRepository.Authenticate(userModel);

            if (validUser != null)
            {
                //Aşağıdaki metot bu client uygulamasının kendi backendi ile başka bir web servise bağımlı olmadan Token oluşturmasını sağlar. Bizim şu anki senaryomuzda bizim için gerekli tokeni MainWenService oluşturduğu için bunu iptal ediyor ve hemen altındaki metodu kullanarak oluşturuyorum.
                //generatedToken = _tokenService.BuildToken(validUser, HttpContext.Connection);

                generatedToken =  _tokenService.BuildToken(validUser, HttpContext.Connection);

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
