
using MongoDB.Driver;

namespace MiraiAnimation.Model.Services {
	public class BluRayService : IDbService<BluRay, string> {
		private IQueryable<BluRay> _blurayCollection;
		private IQueryable<Animation> _animCollection;
		public BluRayService(IMongoDatabase db) {
			_blurayCollection = db.GetCollection<BluRay>("blu-rays").AsQueryable();
			_animCollection = db.GetCollection<Animation>("animations").AsQueryable();
		}
		public bool AddElement(BluRay element) {
			throw new NotImplementedException();
		}

		public BluRay EditElement(BluRay element) {
			throw new NotImplementedException();
		}

		public IEnumerable<BluRay> GetAll() {
			return _blurayCollection.GroupJoin(_animCollection,
				bluRay => bluRay.animazione,
				anim => anim.id,
				(bluRay, anim) =>
					new BluRay { 
						id = bluRay.id, 
						prezzo = bluRay.prezzo,
						descrizione = bluRay.descrizione,
						immagine = bluRay.immagine,
						anim = anim.ElementAt(0)
					}
			).ToList();
			//return _blurayCollection.ToList();
		}

		public BluRay GetById(string id) {
			throw new NotImplementedException();
		}

		public BluRay GetByProperty(string property) {
			throw new NotImplementedException();
		}

		public bool RemoveElement(BluRay element) {
			throw new NotImplementedException();
		}
	}
}
