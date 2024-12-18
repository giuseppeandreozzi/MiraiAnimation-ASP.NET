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

        public ManageStaffModel(IDbService<Staff, string> staffService) {
            _staffService = staffService;
        }

        public void OnGet() {
            Staffs = _staffService.GetAll();
        }

        public IActionResult OnPostEdit(Staff staff) {
            _staffService.EditElement(staff);

            return new RedirectToPageResult("/Admin/ManageStaff");
        }

        public IActionResult OnPostAdd(Staff staff) {
            _staffService.AddElement(staff);

            return new RedirectToPageResult("/Admin/ManageStaff");
        }

        public IActionResult OnPostDelete(string id) {
            _staffService.RemoveElement(_staffService.GetById(id));

            return new RedirectToPageResult("/Admin/ManageStaff");
        }
    }
}
