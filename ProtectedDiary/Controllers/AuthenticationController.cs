using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Mvc;

namespace ProtectedDiary.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController()
        {
        }

        [HttpGet("/auth/signin")]
        public IActionResult SignIn()
        {
            return Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = $"https://{this.Request.Host}/",
                    IsPersistent = true
                },
                TwitterDefaults.AuthenticationScheme);
        }

        [HttpPost("/auth/signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}