using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages.Admin {
	[Authorize(Policy = "AdminRole")]
	public class ManageBluRayModel : PageModel {
        private readonly IDbService<BluRay, string> _bdService;
        private readonly IDbService<Animation, string> _animService;

        public IEnumerable<BluRay> Bds { get; set; }
        public IEnumerable<Animation> Animations { get; set; }

        public ManageBluRayModel(IDbService<BluRay, string> bdService, IDbService<Animation, string> animService) {
            _bdService = bdService;
            _animService = animService;
        }
        public void OnGet() {
            Bds = _bdService.GetAll();
            Animations = _animService.GetAll();
        }

        public async Task<IActionResult> OnPostEdit(BluRay bd, IFormFile immagine) {
            BluRay _bd = _bdService.GetById(bd.id.ToString());

            _bd.anim = null;
            _bd.prezzo = bd.prezzo;
            _bd.descrizione = bd.descrizione;

            if(immagine != null) {
                using (MemoryStream ms = new MemoryStream()) {
                    await immagine.CopyToAsync(ms);
                    _bd.immagine = ms.ToArray();
                }
            }

            _bdService.EditElement(_bd);

            return new RedirectToPageResult("/Admin/ManageBluRay");
        }

        public async Task<IActionResult> OnPostAdd(BluRay bd, IFormFile immagine) {
            if (immagine != null) {
                using (MemoryStream ms = new MemoryStream()) {
                    await immagine.CopyToAsync(ms);
                    bd.immagine = ms.ToArray();
                }
            }

            _bdService.AddElement(bd);

            return new RedirectToPageResult("/Admin/ManageBluRay");
        }

        public IActionResult OnPostDelete(string id) {
            _bdService.RemoveElement(_bdService.GetById(id));

            return new RedirectToPageResult("/Admin/ManageBluRay");
        }
    }
}
