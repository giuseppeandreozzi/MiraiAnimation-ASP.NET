using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages {
    public class AnimationModel : PageModel {
		public IEnumerable<Animation> animations;
        private IDbService<Animation, string> _animService;

        public AnimationModel(IDbService<Animation, string> animService) {
            _animService = animService;
        }

		public void OnGet() {
            animations = _animService.GetAll();
        }
    }
}
