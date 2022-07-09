using Microsoft.AspNetCore.Mvc;

namespace AVSGLOBAL.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginPageVue()
        {
            return View();
        }

        public IActionResult LoginPageReact()
        {
            return View();
        }        
    }
}
