using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson;

namespace MiraiAnimation.Pages.Users {
    [Authorize]
    public class AccountModel : PageModel {
        private readonly IDbService<User, string> _userCollection;

        public AccountModel(IDbService<User, string> userCollection) {
            _userCollection = userCollection;
        }

        public void OnGet() {

        }

        public void OnPost(User user) {
            user.id = new ObjectId(User.FindFirst("id").Value);

			User userToReplace = _userCollection.GetById(user.id.ToString());
            user.email = userToReplace.email;
            user.username = userToReplace.username;
            user.password = userToReplace.password;
            user.tipo = userToReplace.tipo;
            user.verificato = userToReplace.verificato;
            user.indirizzo.id = userToReplace.indirizzo.id;

			_userCollection.EditElement(user);
        }
    }
}
