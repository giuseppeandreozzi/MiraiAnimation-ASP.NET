using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace MiraiAnimation.Pages {
    public class ShopModel : PageModel {
		public IEnumerable<BluRay> bds { get; set; }

        private readonly IDbService<BluRay, string> _bluRayService;
		private readonly IDbService<User, string> _userService;

		public ShopModel(IDbService<BluRay, string> bluRayService, IDbService<User, string> userService) {
            _bluRayService = bluRayService;
            _userService = userService;
        }

		public void OnGet() {
            bds = _bluRayService.GetAll();
        }

        public IActionResult OnPostAdd(string id, string prezzo, string quantità) {
            User user = _userService.GetById(User.FindFirst("id").Value);

            user.carrello.Add(new CartElement {
                Id = (MongoDB.Bson.ObjectId) ObjectIdGenerator.Instance.GenerateId(user.carrello, user),
                bluRay = _bluRayService.GetById(id),
                prezzo = int.Parse(prezzo),
                quantità = int.Parse(quantità)
            });

            _userService.EditElement(user);

            Console.WriteLine(user.id);
            return new RedirectToPageResult("Shop");
        }
    }
}
