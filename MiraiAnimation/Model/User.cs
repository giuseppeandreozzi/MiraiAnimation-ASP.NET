using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MiraiAnimation.Model {
	public class User {
		[BsonId]
		public ObjectId Id { get; set; }
		[BsonElement("nome")]
		public string Nome { get; set; }
		[BsonElement("cognome")]
		public string Cognome { get; set; }
		[BsonElement("username")]
		public string Username { get; set;}
		[BsonElement("password")]
		public string Password { get; set;}
		[BsonElement("email")]
		public string Email { get; set;}
		[BsonElement("dataNascita")]
		public DateTime DataNascita { get; set;}
		[BsonElement("indirizzo")]
		public Indirizzo Indirizzo { get; set;}
		[BsonElement("tipo")]
		public string Tipo { get; set;}
		[BsonElement("verificato")]
		public bool Verificato { get; set;}

	}

	public class Indirizzo {
		[BsonId]
		public ObjectId Id { get; set; }
		[BsonElement("città")]
		public string Città { get; set; }
		[BsonElement("CAP")]
		public string CAP { get; set; }
		[BsonElement("via")]
		public string Via { get; set; }
	}
}
