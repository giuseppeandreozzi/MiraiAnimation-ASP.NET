using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MiraiAnimation.Pages.Users {
    public class VerifyMailModel : PageModel {
		private readonly IDbService<User, string> _usersCollection;

        public bool Success { get; set; } = false;
		public bool AlreadyVerified { get; set; } = false;
		public VerifyMailModel(IDbService<User, string> usersCollection) {
            _usersCollection = usersCollection;
        }
        public async void OnGet(string token, string id) {
            User user = _usersCollection.GetById(id);

            if (user.verificato) {
                AlreadyVerified = true;
                return;
            }

            if(token.Equals(user.datiVerifica.token) && user.datiVerifica.scadenza < DateTime.Now) {
                Success = true;
                user.verificato = true;
                user.datiVerifica = null;
                _usersCollection.EditElement(user);

                if(User.Claims.Count() > 0) {
					((ClaimsIdentity)User.Identity).RemoveClaim(User.FindFirst("Verificato"));
					((ClaimsIdentity)User.Identity).AddClaim(new Claim("Verificato", user.verificato.ToString()));

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, User, new AuthenticationProperties());
				}
			}
        }
    }
}
