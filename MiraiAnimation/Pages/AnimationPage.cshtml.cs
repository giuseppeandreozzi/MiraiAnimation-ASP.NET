using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson;

namespace MiraiAnimation.Pages
{
    public class AnimationPageModel : PageModel {
        private IDbService<Animation> _animService;
		public Animation Animation { get; set; }

		public AnimationPageModel(IDbService<Animation> animService) {
			_animService = animService;
		}
		public void OnGet(string id) {
			Animation = _animService.GetById(id);
		}


	}
}