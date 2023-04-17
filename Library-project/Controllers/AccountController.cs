using Library_project.Models;
using System.Security.Claims;

namespace Library_project.Controllers
{
    public class AccountController: Controller
    {
        public async Task Login(string returnUrl="/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index","Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult UserProfile()
        {
            return View(new student()
            {
                name=User.Identity.Name,
                Email=User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                id=User.FindFirst(c=> c.Type == ClaimTypes.NameIdentifier)?.Value

            });
        }

    }
}
