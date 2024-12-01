using MongoDB.Bson;

namespace MiraiAnimation.Model {
	public class BluRay {
		public ObjectId id {  get; set; }
		public ObjectId animazione { get; set; }
		public int prezzo { get; set; }
		public string descrizione { get; set; }
	}
}
