using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Site24x7.Web.Models;

namespace Site24x7.Web.Controllers
{
    public class HomeController : ParentController
    {
        public HomeController(IOptions<AppSettings> settings) : base(settings)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            ClearSession();
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Logout()
        {
            ClearSession();
            return RedirectToAction("Login", "Account");
        }
    }
}
