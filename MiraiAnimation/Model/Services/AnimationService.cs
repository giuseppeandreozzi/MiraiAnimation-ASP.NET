using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MiraiAnimation.Model.Services {
	public class AnimationService : IDbService<Animation, string> {
		private IMongoCollection<Animation> _animCollection;
		public AnimationService(IMongoDatabase db) {
			_animCollection = db.GetCollection<Animation>("animations");
		}

		public bool AddElement(Animation anim) {
			Task task = _animCollection.InsertOneAsync(anim);

			return !task.IsFaulted;
		}

		public Animation EditElement(Animation anim) {
			var filter = Builders<Animation>.Filter.Eq(_anim => _anim.id, anim.id);

			return _animCollection.FindOneAndReplace(filter, anim);
		}

		public IEnumerable<Animation> GetAll() {
			return _animCollection.AsQueryable().ToList();
		}

		public Animation GetById(string id) {
			return _animCollection.AsQueryable().Where(a => a.id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();
		}

		public Animation GetByProperty(string property) {
			throw new NotImplementedException();
		}

		public bool RemoveElement(Animation anim) {
			var filter = Builders<Animation>.Filter.Eq(_anim => _anim.id, anim.id);
			return _animCollection.DeleteOne(filter).DeletedCount == 1;
		}
	}
}
