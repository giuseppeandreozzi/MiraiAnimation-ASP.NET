using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;

namespace MiraiAnimation.Pages.Users {
    [Authorize]
    public class OrdersModel : PageModel {
        private readonly IDbService<User, string> _userService;

        public IEnumerable<OrderElement> Orders { get; set; }

        public OrdersModel(IDbService<User, string> userService) {
            _userService = userService;
        }

        public void OnGet() {
            User user = _userService.GetById(User.FindFirst("id").Value);

            Orders = user.ordini;
        }
    }
}
