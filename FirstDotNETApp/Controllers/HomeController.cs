using FirstDotNETApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstDotNETApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string? userId =
                HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

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
}
