namespace MiraiAnimation.Model.Services {
	public class AnimationService : IDbService<Animation> {
		private MyDbContext _db;
		public AnimationService(MyDbContext db) {
			_db = db;
		}

		public bool AddElement(Animation element) {
			throw new NotImplementedException();
		}

		public Animation EditElement(Animation element) {
			throw new NotImplementedException();
		}

		public IEnumerable<Animation> GetAll() {
			return _db.Animation.ToList();
		}

		public Animation GetById(string id) {
			throw new NotImplementedException();
		}

		public bool RemoveElement(Animation element) {
			throw new NotImplementedException();
		}
	}
}
