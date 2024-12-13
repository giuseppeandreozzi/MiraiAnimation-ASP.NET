using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages.Users {
    [Authorize]
    public class CartModel : PageModel {
        private readonly IDbService<User, string> _userCollection;
		private readonly IDbService<Animation, string> _animCollection;

		public IEnumerable<CartElement> Carrello { get; set; }

        public CartModel(IDbService<User, string> userCollection, IDbService<Animation, string> animCollection) {
            _userCollection = userCollection;
            _animCollection = animCollection;
        }
        public void OnGet() {
            User user = _userCollection.GetById(User.FindFirst("id").Value);
            
            foreach (CartElement el in user.carrello) {
                el.bluRay.anim = _animCollection.GetById(el.bluRay.animazione.ToString());
            }

            Carrello = user.carrello;
        }
    }
}
