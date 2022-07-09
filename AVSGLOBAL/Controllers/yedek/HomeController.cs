using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AVSGLOBAL.Class.Models;
using AVSGLOBAL.Class.Dal;

namespace AVSGLOBAL.Controllers;

public class HomeyedekController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeyedekController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        DatabaseContext db = new DatabaseContext();
        Mdl_User Kullanici = new Mdl_User();        
            
            Kullanici.Password = "asar";
            Kullanici.Name = "samet";
            Kullanici.Email = "sametasar@gmail.com";
            Kullanici.Create_Date = DateTime.Now;
            Kullanici.LastIP = "127.0.0.1";
            Kullanici.LastLoginTime = DateTime.Now;
            Kullanici.LastToken = "432823094329048093284";


            db.User.Add(Kullanici);
            db.SaveChanges();


        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
