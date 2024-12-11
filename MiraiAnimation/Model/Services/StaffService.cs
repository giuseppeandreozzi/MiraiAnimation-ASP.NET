
using MongoDB.Driver;

namespace MiraiAnimation.Model.Services {
	public class StaffService : IDbService<Staff, string> {
		private IQueryable<Staff> _staffCollection;
		public StaffService(IMongoDatabase db) {
			_staffCollection = db.GetCollection<Staff>("staffs").AsQueryable();
		}
		public bool AddElement(Staff element) {
			throw new NotImplementedException();
		}

		public Staff EditElement(Staff element) {
			throw new NotImplementedException();
		}

		public IEnumerable<Staff> GetAll() {
			return _staffCollection.ToList();
		}

		public Staff GetById(string id) {
			throw new NotImplementedException();
		}

		public Staff GetByProperty(string property) {
			throw new NotImplementedException();
		}

		public bool RemoveElement(Staff element) {
			throw new NotImplementedException();
		}
	}
}
