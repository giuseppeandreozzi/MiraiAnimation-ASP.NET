using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using System.Security.Claims;

namespace MiraiAnimation.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly PasswordHasher<User> _passHasher;
        private readonly IDbService<User, string> _usersCollection;

		public IndexModel(ILogger<IndexModel> logger, PasswordHasher<User> passHasher, IDbService<User, string> usersCollection) {
            _logger = logger;
            _passHasher = passHasher;
            _usersCollection = usersCollection;
        }

        public void OnGet() {

        }

        public async Task<IActionResult> OnPostLogin(string username, string password) {
            User user;
            if((user = _usersCollection.GetByProperty(username)) != null && _passHasher.VerifyHashedPassword(user, user.password, password) == PasswordVerificationResult.Success) {
                List<Claim> claims = [
                    new Claim("id", user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim("Nome", user.nome),
                    new Claim("Cognome", user.cognome),
                    new Claim("Email", user.email),
                    new Claim("DataNascita", user.dataNascita.ToString("yyyy/MM/dd")),
                    new Claim("Indirizzo", user.indirizzo.ToString()),
                    new Claim("Tipo", user.tipo),
                    new Claim("Verificato", user.verificato.ToString())
                ];

				ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());

                return new RedirectToPageResult("Index");
			}

            return Page();
        }

        public IActionResult OnPostSignup(User user) {
            user.tipo = "utente";
            user.password = _passHasher.HashPassword(user, user.password);

            if (_usersCollection.AddElement(user)) {
                return new RedirectToPageResult("Index");
            }

            return Page();

        }

        public async Task<IActionResult> OnGetLogout() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return new RedirectToPageResult("Index");
		}
    }
}
