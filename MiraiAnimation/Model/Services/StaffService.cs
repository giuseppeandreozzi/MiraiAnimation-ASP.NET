
using MongoDB.Driver;

namespace MiraiAnimation.Model.Services {
	public class StaffService : IDbService<Staff, string> {
		private IMongoCollection<Staff> _staffCollection;
		public StaffService(IMongoDatabase db) {
			_staffCollection = db.GetCollection<Staff>("staffs");
		}
		public bool AddElement(Staff staff) {
			Task task = _staffCollection.InsertOneAsync(staff);

			return !task.IsFaulted;
		}

		public Staff EditElement(Staff staff) {
			var filter = Builders<Staff>.Filter.Eq(_staff => _staff.id, staff.id);

			return _staffCollection.FindOneAndReplace(filter, staff);
		}

		public IEnumerable<Staff> GetAll() {
			return _staffCollection.AsQueryable().ToList();
		}

		public Staff GetById(string id) {
			return _staffCollection.AsQueryable().Where(staff => staff.id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();
		}

		public Staff GetByProperty(string property) {
			throw new NotImplementedException();
		}

		public bool RemoveElement(Staff staff) {
			var filter = Builders<Staff>.Filter.Eq(_staff => _staff.id, staff.id);

			return _staffCollection.DeleteOne(filter).DeletedCount == 1;
		}
	}
}
