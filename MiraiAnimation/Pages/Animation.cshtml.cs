using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages {
    public class AnimationModel : PageModel {
		public IEnumerable<Animation> animations;
        private IDbService<Animation> _animService;

        public AnimationModel(IDbService<Animation> animService) {
            _animService = animService;
        }

		public void OnGet() {
            animations = _animService.GetAll();
        }
    }
}
