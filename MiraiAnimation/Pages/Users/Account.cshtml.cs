using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MiraiAnimation.Pages.Users {
    [Authorize]
    public class AccountModel : PageModel {
        private readonly IDbService<User, string> _userCollection;
        [BindProperty]
        public AccountInputModel user { get; set; }

        public AccountModel(IDbService<User, string> userCollection) {
            _userCollection = userCollection;
        }

        public void OnGet() {

        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }

			User userToReplace = _userCollection.GetById(User.FindFirst("id").Value);
            userToReplace.nome = user.nome;
			userToReplace.cognome = user.cognome;
            userToReplace.dataNascita = user.dataNascita;
            userToReplace.indirizzo = user.indirizzo;
            userToReplace.indirizzo.id = (ObjectId) ObjectIdGenerator.Instance.GenerateId(userToReplace, userToReplace.indirizzo);

			_userCollection.EditElement(userToReplace);

            //updating the current ClaimsPrincipal with the new data
            var userIdentity = User.Identity as ClaimsIdentity;

            userIdentity.RemoveClaim(userIdentity.FindFirst("Nome"));
			userIdentity.RemoveClaim(userIdentity.FindFirst("Cognome"));
			userIdentity.RemoveClaim(userIdentity.FindFirst("DataNascita"));
			userIdentity.RemoveClaim(userIdentity.FindFirst("Indirizzo"));

            userIdentity.AddClaim(new Claim("Nome", userToReplace.nome));
			userIdentity.AddClaim(new Claim("Cognome", userToReplace.cognome));
			userIdentity.AddClaim(new Claim("DataNascita", userToReplace.dataNascita.ToString("yyyy/MM/dd")));
			userIdentity.AddClaim(new Claim("Indirizzo", userToReplace.indirizzo.ToString()));

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), new AuthenticationProperties());

			return Page();
        }
    }

    public class AccountInputModel {
		[Required]
		[MaxLength(20)]
		public string nome { get; set; }
		[Required]
		[MaxLength(20)]
		public string cognome { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateTime dataNascita { get; set; }
		[Required]
		public Indirizzo indirizzo { get; set; }
	}
}
