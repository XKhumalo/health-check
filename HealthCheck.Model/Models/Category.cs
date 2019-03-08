using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HealthCheck.Model
{
    public class Category : MongoEntity
    {
        [DataMember]
        [BsonElement("Name")]
        [Display(Name = "Name")]
        [BsonRequired]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [BsonElement("Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataMember]
        [BsonElement("Positive")]
        [Display(Name = "Positive")]
        public string Positive { get; set; }

        [DataMember]
        [BsonElement("Negative")]
        [Display(Name = "Negative")]
        public string Negative { get; set; }
    }
}
