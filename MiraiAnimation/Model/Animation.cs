using MongoDB.Bson;

namespace MiraiAnimation.Model {
	public class Animation {
		public ObjectId id { get; set; }
		public string titolo { get; set; }
		public string genere { get; set; }
		public string tipo { get; set; }
		public DateTime dataUscita { get; set; }

	}
}
