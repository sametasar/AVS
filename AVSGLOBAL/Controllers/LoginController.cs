using Microsoft.AspNetCore.Mvc;
using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Class.GlobalClass;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


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

                validUser.LastToken = generatedToken;  //Servis tarafında servis tokeni bu objeye verilmişti. Şimdide client tarafının tokenini veriyorum.

                validUser.LastIP = HttpContext.Connection.RemoteIpAddress.ToString();

                //Bu noktada kullanıcının ClientTokeninide veri tabanında güncelliyorum.
                await new Cls_User().UpdateClientTokenAndIP(validUser);

                //Kullanıcı bilgilerimi bir session içine şifreleyerek atıyorum. Başka sayfalarda kullanmak için decrypt yaparak kullanabilirsin!
                HttpContext.Session.SetString("UserInfo", Cls_Tools.EnCrypt(JsonConvert.SerializeObject(validUser)));

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
