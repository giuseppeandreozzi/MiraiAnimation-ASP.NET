using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MiraiAnimation.Model.Services {
	public class AnimationService : IDbService<Animation> {
		private IQueryable<Animation> _animCollection;
		public AnimationService(IMongoDatabase db) {
			_animCollection = db.GetCollection<Animation>("animations").AsQueryable();
		}

		public bool AddElement(Animation element) {
			throw new NotImplementedException();
		}

		public Animation EditElement(Animation element) {
			throw new NotImplementedException();
		}

		public IEnumerable<Animation> GetAll() {
			return _animCollection.ToList();
		}

		public Animation GetById(string id) {
			return _animCollection.Where(a => a.id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();
		}

		public bool RemoveElement(Animation element) {
			throw new NotImplementedException();
		}
	}
}
