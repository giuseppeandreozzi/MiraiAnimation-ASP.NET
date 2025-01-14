using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;

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

        public IActionResult OnGetInvoice(string id) {
			User user = _userService.GetById(User.FindFirst("id").Value);
			string htmlProducts = "";
            decimal total = 0;

            OrderElement order = user.ordini.AsQueryable().Where(order => order.Id.ToString() == id).First();

            return File(CreateDocument(order), "application/pdf", $"invoice {order.Id}.pdf");
        }

        private byte[] CreateDocument(OrderElement order) {
            string products = "";
			decimal total = 0;

			foreach (CartElement el in order.prodotti) {
				products += $"Titolo: {el.bluRay.anim.titolo}\nPrezzo: {el.prezzo}\nQuantità: {el.quantità}\n";
				total += el.prezzo;
			}

            return QuestPDF.Fluent.Document.Create(container => {
                container.Page(page => {
					page.Size(PageSizes.A4);
					page.Margin(2, Unit.Centimetre);
					page.PageColor(Colors.White);
					page.DefaultTextStyle(x => x.FontSize(20));

					page.Header()
			            .Text($"FATTURA ORDINE #{order.Id}")
			            .SemiBold().FontSize(30);

                    page.Content()
                        .Column(x => {
                            x.Item().Text(products);
                        });

                    page.Footer()
                        .AlignRight()
                        .Text($"Total: {total}");
				});
            }).GeneratePdf();
		}
    }
}
