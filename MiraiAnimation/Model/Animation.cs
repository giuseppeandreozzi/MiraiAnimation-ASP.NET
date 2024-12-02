using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System.ComponentModel.DataAnnotations;

namespace MiraiAnimation.Model {
	public class Animation {
		public ObjectId id { get; set; }
		public string titolo { get; set; }
		public string genere { get; set; }
		public string tipo { get; set; }
		public DateTime dataUscita { get; set; }
		public byte[]? immagine { get; set; }

	}
}
