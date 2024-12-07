using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MiraiAnimation.Model
{
	[BsonIgnoreExtraElements]
	public class Staff
    {
        public ObjectId id { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string ruolo { get; set; }
        public int anniServizio { get; set; }
    }
}
