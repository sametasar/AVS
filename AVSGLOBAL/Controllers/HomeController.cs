using AVSGLOBAL.Models.GlobalModel;
using AVSGLOBAL.Class.GlobalClass;
using AVSGLOBAL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMTML;


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

        
        [Authorize]
        [Route("mainwindow")]
        [HttpGet]
        public IActionResult MainWindow(string name)
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Index"));
            }

            if (!_tokenService.IsTokenValid(token))
            {
                return (RedirectToAction("Index"));
            }              

            return new Cls_React(new List<string>{"MainPageReact"}).View();    
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