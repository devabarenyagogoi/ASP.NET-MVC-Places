using FirstDotNETApp.Interfaces;
using FirstDotNETApp.Models;
using FirstDotNETApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstDotNETApp.APIController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public AuthApiController(
            IConfiguration configuration,
            IUserService userService,
            IUserRepository userRepository)
        {
            _configuration = configuration;
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.ValidateUser(
                request.UserId,
                request.Password);

            if (user == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid Credentials"
                });
            }

            string token = _userService.GenerateToken(user.Username);

            var userToken = new UserToken
            {
                UserId = user.UserId,
                Token = token,
                IssuedAt = DateTime.Now,
                ExpiryAt = DateTime.Now.AddHours(
                    _configuration.GetValue<int>("TokenSettings:ExpiryHours")),
                IsActive = true
            };

            _userRepository.SaveToken(userToken);

            HttpContext.Session.SetString("UserId", user.Username);
            HttpContext.Session.SetString("Token", token);

            return Ok(new
            {
                Token = token
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            string? token = null;

            // Bearer token
            string? authHeader = Request.Headers["Authorization"]
                .FirstOrDefault();

            token = _userService.ExtractBearerToken(authHeader);

            // Session fallback
            /*if (string.IsNullOrEmpty(token))
            {
                token = HttpContext.Session.GetString("Token");
            }*/

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new
                {
                    Message = "No token present."
                });
            }

            _userRepository.DeactivateToken(token);

            HttpContext.Session.Clear();

            return Ok(new
            {
                Message = "Logged out successfully"
            });
        }

        [HttpPost("validate")]
        public IActionResult ValidateToken([FromBody] string token)
        {
            bool isValid = _userService.IsTokenValid(token);

            return Ok(new
            {
                IsValid = isValid
            });
        }
    }
}