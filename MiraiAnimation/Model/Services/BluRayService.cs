
using MongoDB.Driver;

namespace MiraiAnimation.Model.Services {
	public class BluRayService : IDbService<BluRay, string> {
		private IMongoCollection<BluRay> _blurayCollection;
		private IQueryable<Animation> _animCollection;
		public BluRayService(IMongoDatabase db) {
			_blurayCollection = db.GetCollection<BluRay>("blu-rays");
			_animCollection = db.GetCollection<Animation>("animations").AsQueryable();
		}
		public bool AddElement(BluRay bluray) {
			Task task = _blurayCollection.InsertOneAsync(bluray);

			return !task.IsFaulted;
		}

		public BluRay EditElement(BluRay bd) {
			var filter = Builders<BluRay>.Filter.Eq("id", bd.id);

			return _blurayCollection.FindOneAndReplace(filter, bd);
		}

		public IEnumerable<BluRay> GetAll() {
			return _blurayCollection.AsQueryable().GroupJoin(_animCollection,
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
		}

		public BluRay GetById(string id) {
			BluRay bd = _blurayCollection.AsQueryable().Where(bd => bd.id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();
			bd.anim = _animCollection.Where(animation => animation.id == bd.animazione).FirstOrDefault();

			return bd;
        }

		public BluRay GetByProperty(string property) {
			throw new NotImplementedException();
		}

		public bool RemoveElement(BluRay element) {
			var filter = Builders<BluRay>.Filter.Eq(bd => bd.id, element.id);

			return _blurayCollection.DeleteOne(filter).DeletedCount == 1;
		}
	}
}
