using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using System.Security.Cryptography;

namespace MiraiAnimation.Pages.Users {
    public class ResetPasswordModel : PageModel{
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IMailjetClient _mailClient;
        private readonly IConfiguration _configuration;
        private readonly IDbService<User, string> _userService;

        public string Id { get; set; }
        public bool Succeeded { get; set; } = false;

        public ResetPasswordModel(IMailjetClient mailClient, IConfiguration configuration, IDbService<User, string> userService, PasswordHasher<User> passwordHasher) { 
            _mailClient = mailClient;
            _configuration = configuration;
            _userService = userService;
            _passwordHasher = passwordHasher;
        }
        public IActionResult OnGet(string token, string id) {
            User user = _userService.GetById(id);

            if(user == null || (!token.Equals(user.datiVerifica.token) || user.datiVerifica.scadenza > DateTime.Now)) {
                return BadRequest();
            }

            Id = user.id.ToString();

            return Page();
        }

        public void OnPostReset(string id, string oldPassword, string newPassword, string newPassword2) {
            User user = _userService.GetById(id);

            if(_passwordHasher.VerifyHashedPassword(user, user.password, oldPassword) == PasswordVerificationResult.Success) {
                if(newPassword == newPassword2) {
                    user.password = _passwordHasher.HashPassword(user, newPassword);
                    user.datiVerifica = null;
                    _userService.EditElement(user);
                    Succeeded = true;

                    return;
                }
            }
        }
    }
}
