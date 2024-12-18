using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MiraiAnimation.Model {
	[BsonIgnoreExtraElements]
	public class BluRay {
		public ObjectId id {  get; set; }
		public ObjectId animazione { get; set; }
		public int prezzo { get; set; }
		public string descrizione { get; set; }
		public byte[] immagine { get; set; }
		[BsonIgnoreIfNull]
		public Animation anim {  get; set; } //serve solo per lavorare qui nel codice, ma non sul db
	}
}
