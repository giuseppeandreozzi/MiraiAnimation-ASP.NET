using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MiraiAnimation.Model {
	[BsonIgnoreExtraElements]
	public class BluRay {
		public ObjectId id {  get; set; }
		public ObjectId animazione { get; set; }
		[Required]
		[Range(1,999)]
		public int prezzo { get; set; }
		[Required]
		[Length(1, 255)]
		public string descrizione { get; set; }
		public byte[] immagine { get; set; }
		[BsonIgnoreIfNull]
		public Animation anim {  get; set; } //serve solo per lavorare qui nel codice, ma non sul db
	}
}
