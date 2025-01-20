using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages.Admin {
	[Authorize(Policy = "AdminRole")]
	public class ManageStaffModel : PageModel {
        private readonly IDbService<Staff, string> _staffService;

        public IEnumerable<Staff> Staffs { get; set; }

        [BindProperty]
        public Staff Staff { get; set; }

        public ManageStaffModel(IDbService<Staff, string> staffService) {
            _staffService = staffService;
        }

        public void OnGet() {
            Staffs = _staffService.GetAll();
        }

        public IActionResult OnPostEdit() {
            if(!ModelState.IsValid) {
                Staffs = _staffService.GetAll();
                return Page();
            }

            _staffService.EditElement(Staff);

            return new RedirectToPageResult("/Admin/ManageStaff");
        }

        public IActionResult OnPostAdd() {
            if (!ModelState.IsValid) {
                Staffs = _staffService.GetAll();
                return Page();
            }

            _staffService.AddElement(Staff);

            return new RedirectToPageResult("/Admin/ManageStaff");
        }

        public IActionResult OnPostDelete(string id) {
            _staffService.RemoveElement(_staffService.GetById(id));

            return new RedirectToPageResult("/Admin/ManageStaff");
        }
    }
}
