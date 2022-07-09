using AVSGLOBAL.Class.Models;
using AVSGLOBAL.Class.Global;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AVSGLOBAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUser _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
        public HomeController(IConfiguration config, ITokenService tokenService, IUser userRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpGet]
        public IActionResult Login(string UserName,string Password)
        {

            Mdl_User userModel = new Mdl_User();
            userModel.Name = UserName;
            userModel.Password = Password;


            if (string.IsNullOrEmpty(userModel.Name) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            IActionResult response = Unauthorized();
            var validUser = GetUser(userModel);

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
        private Mdl_User GetUser(Mdl_User userModel)
        {
            //Write your code here to authenticate the user
            return _userRepository.GetUser(userModel);
        }

        [Authorize]
        [Route("mainwindow")]
        [HttpGet]
        public IActionResult MainWindow()
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Index"));
            }

            if (!_tokenService.IsTokenValid(Cls_Settings.JWTKEY,
                Cls_Settings.JWTISSUER, token))
            {
                return (RedirectToAction("Index"));
            }

            ViewBag.Message = BuildMessage(token, 50);
            return View();
        }

        public IActionResult Error()
        {
            ViewBag.Message = "An error occured...";
            return View();
        }
        private string BuildMessage(string stringToSplit, int chunkSize)
        {
            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize)
                .Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));

            string result = "The generated token is:";

            foreach (string str in data)
            {
                result += Environment.NewLine + str;
            }

            return result;
        }
    }
}
