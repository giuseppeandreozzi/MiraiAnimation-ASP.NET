using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MiraiAnimation.Model {
	[BsonIgnoreExtraElements]
	public class Animation {
		public ObjectId id { get; set; }
		[Required]
		[Length(1, 100)]
		public string titolo { get; set; }
		[Required]
		[Length(1, 100)]
		public string genere { get; set; }
		[Required]
		[AllowedValues(["serie", "film"])]
		public string tipo { get; set; }
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime dataUscita { get; set; }
		[ValidateNever]
		public byte[]? immagine { get; set; }
		[ValidateNever]
		[BsonElement("recensioni")]
		public List<Recensione> recensioni { get; set; } = new List<Recensione>();

	}

	public class Recensione {
		public ObjectId user { get; set; }
		public string username { get; set; }
		public int voto { get; set; }
		public string commento { get; set;}
		public ObjectId _id { get; set; }
	}
}
