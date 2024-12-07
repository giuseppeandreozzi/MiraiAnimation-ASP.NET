using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages {
    public class ListaStaffModel : PageModel {
        public IEnumerable<Staff> staffs;
        private IDbService<Staff> _staffService;

        public ListaStaffModel(IDbService<Staff> staffService) {
            _staffService = staffService;
        }

		public void OnGet() {
            staffs = _staffService.GetAll();
        }
    }
}
