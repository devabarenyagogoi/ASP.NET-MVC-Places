using FirstDotNETApp.Filters;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstDotNETApp.Controllers
{
    [TokenAuthorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            string? token =
                HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token) ||
                !_userService.IsTokenValid(token))
            {
                HttpContext.Session.Clear();

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
