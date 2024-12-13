
using MongoDB.Bson;
using MongoDB.Driver;

namespace MiraiAnimation.Model.Services {
	public class UserService : IDbService<User, string> {
		private readonly IMongoCollection<User> _usersCollection;
		public UserService(IMongoDatabase db) { 
			_usersCollection = db.GetCollection<User>("users");
		}
		public bool AddElement(User user) {
			Task task = _usersCollection.InsertOneAsync(user);

			return task.IsFaulted;
		}

		public User EditElement(User user) {
			var filter = Builders<User>.Filter.Eq(_user => _user.id, user.id);

			return _usersCollection.FindOneAndReplace(filter, user);
		}

		public IEnumerable<User> GetAll() {
			throw new NotImplementedException();
		}

		public User GetById(string id) {
			return _usersCollection.AsQueryable().Where(user => user.id == new ObjectId(id)).FirstOrDefault();
		}

		public User GetByProperty(string username) {
			return _usersCollection.AsQueryable().SingleOrDefault(user => user.username == username);
		}

		public bool RemoveElement(User element) {
			throw new NotImplementedException();
		}
	}
}
