using FirstDotNETApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstDotNETApp.Filters
{
    public class TokenAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = context.HttpContext.RequestServices
                .GetService<IUserService>();

            string? token = null;

            // Bearer Token
            string? authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            token = userService?.ExtractBearerToken(authHeader);

            // Session token
            /*if (string.IsNullOrEmpty(token))
            {
                token = context.HttpContext.Session.GetString("Token");
            }*/

            bool isValid =
                !string.IsNullOrEmpty(token) &&
                userService != null &&
                userService.IsTokenValid(token);

            if (!isValid)
            {
                var path =
                    context.HttpContext.Request.Path.Value ?? "";

                // API response
                if (path.StartsWith("/api",
                    StringComparison.OrdinalIgnoreCase))
                {
                    context.Result = new UnauthorizedObjectResult(
                        new
                        {
                            Message = "Unauthorized. Invalid or expired token."
                        });
                }
                // MVC response
                else
                {
                    context.Result =
                        new RedirectToActionResult(
                            "Login",
                            "Account",
                            null);
                }

                return;
            }

            base.OnActionExecuting(context);
        }
    }
}