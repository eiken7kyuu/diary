using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProtectedDiary.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController()
        {
        }

        [HttpGet("/auth/signin")]
        public IActionResult SignIn(string returnUrl = null)
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = $"https://{this.Request.Host}{returnUrl}",
            },
            TwitterDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpPost("/auth/signout")]
        public new async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}