
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

		public User EditElement(User element) {
			throw new NotImplementedException();
		}

		public IEnumerable<User> GetAll() {
			throw new NotImplementedException();
		}

		public User GetById(string id) {
			throw new NotImplementedException();
		}

		public User GetByProperty(string username) {
			return _usersCollection.AsQueryable().SingleOrDefault(user => user.username == username);
		}

		public bool RemoveElement(User element) {
			throw new NotImplementedException();
		}
	}
}
