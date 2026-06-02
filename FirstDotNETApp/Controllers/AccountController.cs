using Microsoft.AspNetCore.Mvc;
using FirstDotNETApp.ViewModels;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        const string USER_ID = "admin";
        const string PASSWORD = "12345";

        if (model.UserId != USER_ID ||
            model.Password != PASSWORD)
        {
            ViewBag.Error = "Invalid Credentials";
            return View(model);
        }

        HttpContext.Session.SetString("UserId", model.UserId);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }
}