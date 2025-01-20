using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages.Admin {
	[Authorize(Policy = "AdminRole")]
	public class ManageUserModel : PageModel {
        private readonly IDbService<User, string> _userCollection;

        public IEnumerable<User> Users { get; set; }

        [BindProperty]
        public User User { get; set; }
        public ManageUserModel(IDbService<User, string> userCollection) {
			_userCollection = userCollection;
        }

        public void OnGet() {
            Users = _userCollection.GetAll();
        }

        public IActionResult OnPostEdit() {
            if (!ModelState.IsValid) {
                Users = _userCollection.GetAll();
                return Page();
            }

            User _user = _userCollection.GetById(User.id.ToString());

            if(User.password != null) {
				PasswordHasher<User> hasher = new PasswordHasher<User>();

                User.password = hasher.HashPassword(User, User.password);
			} else {
                User.password = _user.password;

			}

            User.indirizzo.id = _user.indirizzo.id;
            User.verificato = _user.verificato;
            User.carrello = _user.carrello;
            User.ordini = _user.ordini;
            User.datiVerifica = _user.datiVerifica;

            _userCollection.EditElement(User);

            return new RedirectToPageResult("/Admin/ManageUser");
        }

        public IActionResult OnPostDelete(string id) {
            _userCollection.RemoveElement(_userCollection.GetById(id.ToString()));

			return new RedirectToPageResult("/Admin/ManageUser");
		}
    }
}
