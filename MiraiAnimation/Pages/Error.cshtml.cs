using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiraiAnimation.Pages {
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel {
        private readonly ILogger<ErrorModel> _logger;

        public string errorString { get; set; } = "";

        public ErrorModel(ILogger<ErrorModel> logger) {
            _logger = logger;
        }

        public void OnGet() {
            errorString = "Si è verificato un errore imprevisto. Contattare l'admin del sito.";
        }

        public void OnGetStatus(string errorCode) {
            if (errorCode[0] == '4') {
                errorString = "La risorsa cercata non esiste";
            } else if (errorCode[0] == '5') {
                errorString = "Si è verificato un errore durante l'elaborazione della richiesta";
            }
        }
    }

}
