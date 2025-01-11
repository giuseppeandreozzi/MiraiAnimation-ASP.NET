using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Transactions;

namespace MiraiAnimation.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private readonly PasswordHasher<User> _passHasher;
        private readonly IDbService<User, string> _usersCollection;
        private readonly IMailjetClient _mailClient;
        private readonly IConfiguration _configuration;

		public IndexModel(ILogger<IndexModel> logger, PasswordHasher<User> passHasher, IDbService<User, string> usersCollection, IMailjetClient mailClient, IConfiguration configuration) {
            _logger = logger;
            _passHasher = passHasher;
            _usersCollection = usersCollection;
            _mailClient = mailClient;
            _configuration = configuration;
        }

        public void OnGet() {

        }

        public async Task<IActionResult> OnPostLogin(string username, string password) {
            User user;
            if((user = _usersCollection.GetByProperty(username)) != null && _passHasher.VerifyHashedPassword(user, user.password, password) == PasswordVerificationResult.Success) {
                List<Claim> claims = [
                    new Claim("id", user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim("Nome", user.nome),
                    new Claim("Cognome", user.cognome),
                    new Claim("Email", user.email),
                    new Claim("DataNascita", user.dataNascita.ToString("yyyy/MM/dd")),
                    new Claim("Indirizzo", user.indirizzo.ToString()),
                    new Claim("Tipo", user.tipo),
                    new Claim("Verificato", user.verificato.ToString())
                ];

				ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());

                return new RedirectToPageResult("Index");
			}

            return Page();
        }

        public async Task<IActionResult> OnPostSignup(User user) {
            user.tipo = "utente";
            user.password = _passHasher.HashPassword(user, user.password);
            user.datiVerifica.token = RandomNumberGenerator.GetHexString(16);
            user.datiVerifica.scadenza = DateTime.Now.AddMinutes(10);
            user.verificato = false;

            if (_usersCollection.AddElement(user)) {
                string link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Users/VerifyMail/{user.datiVerifica.token}/{user.id}";

                string mailContent = $"<h2>Verifica e-mail</h2></br>"
                       + $"Clicca sul seguente link per attivare la mail: <a href='{link}'>Verifica mail</a>";
                var mail = new TransactionalEmailBuilder()
                    .WithFrom(new SendContact(_configuration["EMAIL"]))
                    .WithSubject("Verifica e-mail")
                    .WithHtmlPart(mailContent)
                    .WithTo(new SendContact(user.email))
                    .Build();

				await _mailClient.SendTransactionalEmailAsync(mail);
				return new RedirectToPageResult("Index");
            }

            return Page();

        }

        public async Task<IActionResult> OnGetLogout() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return new RedirectToPageResult("Index");
		}

        public async Task<IActionResult> OnGetResetPassword() {
            User user = _usersCollection.GetById(User.FindFirst("id").Value);

            if (user == null) {
                return Page();
            }

            user.datiVerifica = new DatiVerifica();
            user.datiVerifica.token = RandomNumberGenerator.GetHexString(16);
            user.datiVerifica.scadenza = DateTime.Now.AddMinutes(10);

            _usersCollection.EditElement(user);

            string link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Users/ResetPassword/{user.datiVerifica.token}/{user.id}";

            string mailContent = $"<h2>Reimposta password</h2></br>"
                   + $"Clicca sul seguente link per reimpostare la password: <a href='{link}'>Reimposta password</a>";
            var mail = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_configuration["EMAIL"]))
                .WithSubject("Reimposta password")
                .WithHtmlPart(mailContent)
                .WithTo(new SendContact(user.email))
                .Build();

            await _mailClient.SendTransactionalEmailAsync(mail);

            return Page();
        }
    }
}
