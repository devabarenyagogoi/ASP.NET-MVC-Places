using FirstDotNETApp.Data;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using FirstDotNETApp.Repositories;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public AccountController(
        IConfiguration configuration,
        IUserService userService,
        IUserRepository userRepository)
    {
        _configuration = configuration;
        _userService = userService;
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        var user = _userService.ValidateUser(
            model.UserId,
            model.Password);

        if (user == null)
        {
            ViewBag.Error = "Invalid Credentials";
            return View(model);
        }

        string token = _userService.GenerateToken(user.Username);

        var userToken = new UserToken
        {
            UserId = user.UserId,
            Token = token,
            IssuedAt = DateTime.Now,
            ExpiryAt = DateTime.Now.AddHours(_configuration.GetValue<int>("TokenSettings:ExpiryHours")),
            IsActive = true
        };

        _userRepository.SaveToken(userToken);

        HttpContext.Session.SetString("UserId", user.Username);
        HttpContext.Session.SetString("Token", token);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        string? token = HttpContext.Session.GetString("Token");

        if (!string.IsNullOrEmpty(token))
        {
            _userRepository.DeactivateToken(token);
        }

        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }

    private bool ValidateCurrentToken()
    {
        string? token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return false;

        return _userService.IsTokenValid(token);
    }
}