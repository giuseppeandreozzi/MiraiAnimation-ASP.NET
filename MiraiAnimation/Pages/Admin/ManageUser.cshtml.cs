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

        public ManageUserModel(IDbService<User, string> userCollection) {
			_userCollection = userCollection;
        }

        public void OnGet() {
            Users = _userCollection.GetAll();
        }

        public IActionResult OnPostEdit(User user) {
            User _user = _userCollection.GetById(user.id.ToString());

            if(user.password != null) {
				PasswordHasher<User> hasher = new PasswordHasher<User>();

                user.password = hasher.HashPassword(user, user.password);
			} else {
                user.password = _user.password;

			}

            user.indirizzo.id = _user.indirizzo.id;
            user.verificato = _user.verificato;
            user.carrello = _user.carrello;
            user.ordini = _user.ordini;
            user.datiVerifica = _user.datiVerifica;

            _userCollection.EditElement(user);

            return new RedirectToPageResult("/Admin/ManageUser");
        }

        public IActionResult OnPostDelete(string id) {
            _userCollection.RemoveElement(_userCollection.GetById(id.ToString()));

			return new RedirectToPageResult("/Admin/ManageUser");
		}
    }
}
