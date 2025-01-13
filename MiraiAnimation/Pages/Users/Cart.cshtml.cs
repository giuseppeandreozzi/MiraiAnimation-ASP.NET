using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using Stripe.Checkout;

namespace MiraiAnimation.Pages.Users {
    [Authorize]
    public class CartModel : PageModel {
        private readonly IDbService<User, string> _userCollection;
		private readonly IDbService<Animation, string> _animCollection;
		private readonly IDbService<BluRay, string> _bluRayCollection;

		public IEnumerable<CartElement> Carrello { get; set; }

        public CartModel(IDbService<User, string> userCollection, IDbService<Animation, string> animCollection, IDbService<BluRay, string> bluRayCollection) {
            _userCollection = userCollection;
            _animCollection = animCollection;
            _bluRayCollection = bluRayCollection;
        }
        public void OnGet() {
            User user = _userCollection.GetById(User.FindFirst("id").Value);
            
            foreach (CartElement el in user.carrello) {
                el.bluRay.anim = _animCollection.GetById(el.bluRay.animazione.ToString());
            }

            Carrello = user.carrello;
        }

        public IActionResult OnGetCheckout() {
            string domain = $"{Request.Scheme}://{Request.Host}/";

            User user = _userCollection.GetById(User.FindFirst("id").Value);

			List<SessionLineItemOptions> lineItems = new List<SessionLineItemOptions>();

            foreach(CartElement el in user.carrello) {
                lineItems.Add(new SessionLineItemOptions {
                    Quantity = el.quantità,
                    PriceData = new SessionLineItemPriceDataOptions {
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions {
                            Name = el.bluRay.anim.titolo,
                            Description = el.bluRay.descrizione
                        },
                        UnitAmountDecimal = el.prezzo * 100
                    }
                });
            }

			var options = new SessionCreateOptions {
                CustomerEmail = user.email,
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + "Users/OrderSuccess",
                CancelUrl = domain + "Users/Cart"
            };

            SessionService service = new SessionService();
            Session session = service.Create(options);

			Response.Headers.Append("Location", session.Url);
			return new StatusCodeResult(303);
		}
    }
}
