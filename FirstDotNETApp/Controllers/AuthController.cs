using Microsoft.AspNetCore.Mvc;
using FirstDotNETApp.ViewModels;

namespace FirstDotNETApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.UserId == "admin" &&
               model.Password == "admin123")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid User ID or Password";
            return View();
        }
    }
}