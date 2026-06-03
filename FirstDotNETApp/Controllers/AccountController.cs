using FirstDotNETApp.Data;
using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using FirstDotNETApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AccountController(AppDbContext context, IConfiguration configuration, IUserRepository userRepository)
    {
        _context = context;
        _configuration = configuration;
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
        string hashedPassword = HashPassword(model.Password);

        var user = _userRepository.GetUserByUsernameAndPassword(
            model.UserId,
        hashedPassword);

        if (user == null)
        {
            ViewBag.Error = "Invalid Credentials";
            return View(model);
        }

        string token = Guid.NewGuid().ToString();

        var userToken = new UserToken
        {
            UserId = user.UserId,
            Token = token,
            IssuedAt = DateTime.Now,
            ExpiryAt = DateTime.Now.AddHours(_configuration.GetValue<int>("TokenSettings:ExpiryHours")),
            IsActive = true
        };

        _context.UserTokens.Add(userToken);
        _context.SaveChanges();

        HttpContext.Session.SetString("UserId", user.Username);
        HttpContext.Session.SetString("Token", token);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        string? token = HttpContext.Session.GetString("Token");

        if (!string.IsNullOrEmpty(token))
        {
            var userToken = _context.UserTokens
                .FirstOrDefault(t => t.Token == token);

            if (userToken != null)
            {
                userToken.IsActive = false;
                _context.SaveChanges();
            }
        }

        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);

        return Convert.ToHexString(hash).ToLower();
    }
}