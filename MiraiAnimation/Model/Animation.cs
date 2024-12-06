using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MiraiAnimation.Model {
	[BsonIgnoreExtraElements]
	public class Animation {
		public ObjectId id { get; set; }
		public string titolo { get; set; }
		public string genere { get; set; }
		public string tipo { get; set; }
		public DateTime dataUscita { get; set; }
		public byte[]? immagine { get; set; }
		[BsonElement("recensioni")]
		public List<Recensione> recensioni { get; set; }
		//public Staff[] staffs { get; set; }

	}

	public class Recensione {
		public ObjectId user { get; set; }
		public string username { get; set; }
		public int voto { get; set; }
		public string commento { get; set;}
		public ObjectId _id { get; set; }
	}
}
