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
		[BindProperty]
		public Animation Anim {  get; set; }

		public ManageAnimationModel(IDbService<Animation, string> animCollection, IDbService<Staff, string> staffCollection) {
            _animCollection = animCollection;
            _staffCollection = staffCollection;
        }

        public void OnGet() {
            Animations = _animCollection.GetAll();
            Staffs = _staffCollection.GetAll();
        }

        public async Task<IActionResult> OnPostEdit(IFormFile immagine) {
			if(!ModelState.IsValid) {
				Animations = _animCollection.GetAll();
				Staffs = _staffCollection.GetAll();
				return Page();
			}

			Animation animation = _animCollection.GetById(Anim.id.ToString());
            if (immagine != null) {
                using(MemoryStream memoryStream =  new MemoryStream()) {
                    await immagine.CopyToAsync(memoryStream);
					Anim.immagine = memoryStream.ToArray();
				}
			} else {
				Anim.immagine = animation.immagine;
			}

            _animCollection.EditElement(Anim);
            return new RedirectToPageResult("/Admin/ManageAnimation");

        }

		public async Task<IActionResult> OnPostAdd(IFormFile immagine) {
			if (!ModelState.IsValid) {
				Animations = _animCollection.GetAll();
				Staffs = _staffCollection.GetAll();
				return Page();
			}

			if (immagine != null) {
				using (MemoryStream memoryStream = new MemoryStream()) {
					await immagine.CopyToAsync(memoryStream);
					Anim.immagine = memoryStream.ToArray();
				}
			}

			_animCollection.AddElement(Anim);
			return new RedirectToPageResult("/Admin/ManageAnimation");

		}

		public IActionResult OnPostDelete(string id) {
			_animCollection.RemoveElement(_animCollection.GetById(id));
			return new RedirectToPageResult("/Admin/ManageAnimation");

		}
	}
}
