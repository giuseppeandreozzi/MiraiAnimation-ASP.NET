using MongoDB.Bson;

namespace MiraiAnimation.Model
{
    public class Staff
    {
        public ObjectId id { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string ruolo { get; set; }
        public int anniServizio { get; set; }
    }
}
