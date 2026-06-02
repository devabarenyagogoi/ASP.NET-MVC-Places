using Microsoft.AspNetCore.Mvc;
using FirstDotNETApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace FirstDotNETApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<TokenInfo> ActiveTokens = new();

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            const string USER_ID = "admin";
            const string PASSWORD = "12345";

            if (request.UserId != USER_ID ||
                request.Password != PASSWORD)
            {
                return Unauthorized("Invalid Credentials");
            }

            string token = GenerateToken();

            var tokenInfo = new TokenInfo
            {
                UserId = request.UserId,
                Token = token,
                ExpiryTime = DateTime.UtcNow.AddMinutes(30)
            };

            ActiveTokens.Add(tokenInfo);

            return Ok(tokenInfo);
        }

        private string GenerateToken()
        {
            string rawData =
                Guid.NewGuid().ToString() +
                DateTime.UtcNow.Ticks;

            using var sha = SHA256.Create();

            byte[] hash =
                sha.ComputeHash(
                    Encoding.UTF8.GetBytes(rawData));

            return Convert.ToHexString(hash);
        }

        private TokenInfo? GetValidToken(string token)
        {
            var tokenInfo = ActiveTokens
                .FirstOrDefault(t => t.Token == token);

            if (tokenInfo == null)
                return null;

            if (tokenInfo.ExpiryTime < DateTime.UtcNow)
                return null;

            return tokenInfo;
        }

        [HttpGet("validate")]
        public IActionResult Validate(string token)
        {
            if (GetValidToken(token) == null)
                return Unauthorized();

            return Ok("Valid Token");
        }

        [HttpGet("secret")]
        public IActionResult Secret(string token)
        {
            if (GetValidToken(token) == null)
                return Unauthorized();

            return Ok("Welcome Admin");
        }
    }
}