using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MiraiAnimation.Model {
	public class User {
		public ObjectId id { get; set; }
		public string nome { get; set; }
		public string cognome { get; set; }
		public string username { get; set;}
		public string password { get; set;}
		public string email { get; set;}
		public DateTime dataNascita { get; set;}
		public Indirizzo indirizzo { get; set;}
		public string tipo { get; set;}
		public bool verificato { get; set;}
		public IEnumerable<CartElement> carrello { get; set;} = Enumerable.Empty<CartElement>();

	}

	public class Indirizzo {
		public ObjectId id { get; set; }
		public string città { get; set; }
		public string CAP { get; set; }
		public string via { get; set; }

		public override string ToString() {
			return via + ", " + città + ", " + CAP;
		}
	}

	public class CartElement {
		public ObjectId id { get; set; }
		public BluRay bluRay { get; set; }
		public int quantità { get; set; }
		public int prezzo { get; set; }
	}
}
