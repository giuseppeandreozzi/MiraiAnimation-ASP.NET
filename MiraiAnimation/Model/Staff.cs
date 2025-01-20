using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MiraiAnimation.Model
{
	[BsonIgnoreExtraElements]
	public class Staff
    {
        public ObjectId id { get; set; }
        [Required]
        [Length(1, 100)]
        public string nome { get; set; }
        [Required]
        [Length(1, 100)]
        public string cognome { get; set; }
        [Required]
        [AllowedValues(["Direttore", "Staff tecnico", "Regista"])]
        public string ruolo { get; set; }
        [Required]
        [Range(0, 99)]
        public int anniServizio { get; set; }
    }
}
