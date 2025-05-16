using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication3.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var idToken = await HttpContext.GetTokenAsync("id_token");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var keycloakEndSessionUrl =
                "http://localhost:8080/realms/sso/protocol/openid-connect/logout" +
                $"?id_token_hint={idToken}" +
                $"&post_logout_redirect_uri=http://localhost:5000/home";

            return Redirect(keycloakEndSessionUrl);
        }
    }
}
