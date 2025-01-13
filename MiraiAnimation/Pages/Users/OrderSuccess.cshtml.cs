using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MiraiAnimation.Pages.Users {
    [Authorize]
    public class OrderSuccessModel : PageModel {
        private readonly IDbService<User, string> _userService;

        public OrderSuccessModel(IDbService<User, string> userService) {
            _userService = userService;
        }
        public void OnGet() {
            User user = _userService.GetById(User.FindFirst("id").Value);

            user.ordini.Add(new OrderElement {
				Id = (MongoDB.Bson.ObjectId) ObjectIdGenerator.Instance.GenerateId(user.ordini, user),
				prodotti = user.carrello,
                dataAcquisto = DateTime.Now
            });

            user.carrello = new List<CartElement>();

            _userService.EditElement(user);
		}
    }
}
