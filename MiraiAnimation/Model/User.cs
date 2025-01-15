using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MiraiAnimation.Model {
	public class User {
		public ObjectId id { get; set; }
		[Required]
		[MaxLength(20)]
		public string nome { get; set; }
		[Required]
		[MaxLength(20)]
		public string cognome { get; set; }
		[Required]
		[MaxLength(20)]
		public string username { get; set;}
		[Required]
		public string password { get; set;}
		[Required]
		[EmailAddress]
		public string email { get; set;}
		[Required]
		[DataType(DataType.Date)]
		public DateTime dataNascita { get; set;}
		[Required]
		public Indirizzo indirizzo { get; set;}
		[ValidateNever]
		public string tipo { get; set;}
		[ValidateNever]
		public bool verificato { get; set;}
		[ValidateNever]
		public List<CartElement> carrello { get; set;} = new List<CartElement> ();
		[ValidateNever]
		public DatiVerifica? datiVerifica { get; set; } = new DatiVerifica();
		[ValidateNever]
		public List<OrderElement> ordini { get; set; } = new List<OrderElement>();

	}

	public class Indirizzo {
		public ObjectId id { get; set; }
		[Required]
		[MaxLength(20)]
		public string città { get; set; }
		[Required]
		public string CAP { get; set; }
		[Required]
		[MaxLength(30)]
		public string via { get; set; }

		public override string ToString() {
			return via + ", " + città + ", " + CAP;
		}
	}

	public class CartElement {
		public ObjectId Id { get; set; }
		public BluRay bluRay { get; set; }
		public int quantità { get; set; }
		public int prezzo { get; set; }
	}

	public class OrderElement {
		public ObjectId Id { get; set; }
		public List<CartElement> prodotti { get; set; }
		public DateTime dataAcquisto { get; set; }
	}

	public class DatiVerifica {
		public ObjectId Id { get; set; }
		public string token { get; set; }
		public DateTime scadenza { get; set; }
	}
}
