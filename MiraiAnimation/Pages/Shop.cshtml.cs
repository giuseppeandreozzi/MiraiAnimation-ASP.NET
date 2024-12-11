using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiraiAnimation.Model;
using MiraiAnimation.Model.Services;
using MongoDB.Bson.Serialization.Serializers;

namespace MiraiAnimation.Pages {
    public class ShopModel : PageModel {
		public IEnumerable<BluRay> bds { get; set; }

        private IDbService<BluRay> _db;

        public ShopModel(IDbService<BluRay> db) {
            _db = db;
        }

		public void OnGet() {
            bds = _db.GetAll();
        }
    }
}
