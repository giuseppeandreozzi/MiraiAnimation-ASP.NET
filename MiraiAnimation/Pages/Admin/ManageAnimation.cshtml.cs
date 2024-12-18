using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages.Admin {
	[Authorize(Policy = "AdminRole")]
    public class ManageAnimationModel : PageModel {
        private readonly IDbService<Animation, string> _animCollection;
		private readonly IDbService<Staff, string> _staffCollection;

		public IEnumerable<Animation> Animations { get; set; }
		public IEnumerable<Staff> Staffs { get; set; }

		public ManageAnimationModel(IDbService<Animation, string> animCollection, IDbService<Staff, string> staffCollection) {
            _animCollection = animCollection;
            _staffCollection = staffCollection;
        }

        public void OnGet() {
            Animations = _animCollection.GetAll();
            Staffs = _staffCollection.GetAll();
        }

        public async Task<IActionResult> OnPostEdit(Animation anim, IFormFile immagine) {
            if (immagine != null) {
                using(MemoryStream memoryStream =  new MemoryStream()) {
                    await immagine.CopyToAsync(memoryStream);
					anim.immagine = memoryStream.ToArray();
				}
            }

            _animCollection.EditElement(anim);
            return new RedirectToPageResult("/Admin/ManageAnimation");

        }

		public async Task<IActionResult> OnPostAdd(Animation anim, IFormFile immagine) {
			if (immagine != null) {
				using (MemoryStream memoryStream = new MemoryStream()) {
					await immagine.CopyToAsync(memoryStream);
					anim.immagine = memoryStream.ToArray();
				}
			}

			_animCollection.AddElement(anim);
			return new RedirectToPageResult("/Admin/ManageAnimation");

		}

		public IActionResult OnPostDelete(string id) {
			_animCollection.RemoveElement(_animCollection.GetById(id));
			return new RedirectToPageResult("/Admin/ManageAnimation");

		}
	}
}
