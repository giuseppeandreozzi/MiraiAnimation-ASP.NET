using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MiraiAnimation.Pages {
    public class ShopModel : PageModel {
		public IEnumerable<BluRay> bds { get; set; }

        private readonly IDbService<BluRay, string> _bluRayService;
		private readonly IDbService<User, string> _userService;
        [BindProperty]
        public InputModel Input { get; set; }

		public ShopModel(IDbService<BluRay, string> bluRayService, IDbService<User, string> userService) {
            _bluRayService = bluRayService;
            _userService = userService;
        }

		public void OnGet() {
            bds = _bluRayService.GetAll();
        }

        public IActionResult OnPostAdd() {
            if(!ModelState.IsValid) {
				bds = _bluRayService.GetAll();
				return Page();
            }

            User user = _userService.GetById(User.FindFirst("id").Value);

            user.carrello.Add(new CartElement {
                Id = (MongoDB.Bson.ObjectId) ObjectIdGenerator.Instance.GenerateId(user.carrello, user),
                bluRay = _bluRayService.GetById(Input.Id),
                prezzo = Input.Prezzo,
                quantità = Input.Quantità
            });

            _userService.EditElement(user);

            Console.WriteLine(user.id);
            return new RedirectToPageResult("Shop");
        }
    }

    public class InputModel {
        [Required]
        public string Id { get; set; }
		[Required]
        [Range(1, 999)]
		public int Prezzo { get; set; }
		[Required]
		[Range(1, 999)]
		public int Quantità { get; set; }
    }

}
