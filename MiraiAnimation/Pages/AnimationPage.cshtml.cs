using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MiraiAnimation.Pages
{
    public class AnimationPageModel : PageModel {
        private IDbService<Animation, string> _animService;
		public Animation Animation { get; set; }

		public AnimationPageModel(IDbService<Animation, string> animService) {
			_animService = animService;
		}
		public void OnGet(string id) {
			Animation = _animService.GetById(id);
		}

		public IActionResult OnPostAddReview(ReviewInputModel input) {

			if(!ModelState.IsValid) {
				Animation = _animService.GetById(input.Id);
				return Page();
			}

			Animation anim = _animService.GetById(input.Id);

			Recensione recensione = new Recensione {
				user = new ObjectId(HttpContext.User.Claims.First(claim => claim.Type == "id").Value),
				username = HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
				voto = int.Parse(input.Voto),
				commento = input.Commento,
				_id = (MongoDB.Bson.ObjectId) ObjectIdGenerator.Instance.GenerateId(anim.recensioni, anim)
			};

			anim.recensioni.Add(recensione);

			_animService.EditElement(anim);

			return new RedirectToPageResult("/AnimationPage", new { id = input.Id });
		}


		public class ReviewInputModel {
			[Required]
			public string Id { get; set; }
			[Required]
			[Range(1, 5)]
			public string Voto { get; set; }
			[Required]
			[Length(1, 255)]
			public string Commento { get; set; }
		}
	}
}
