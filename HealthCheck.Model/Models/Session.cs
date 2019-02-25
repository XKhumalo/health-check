using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using HealthCheck.Common;

namespace HealthCheck.Model
{
    public class Session : MongoEntity
    {
        [DataMember]
        [BsonElement("SessionKey")]
        [Display(Name = "Session Key")]
        public string SessionKey { get; } = Helpers.RandomString(6, false);

        [DataMember]
        [BsonElement("StartDate")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataMember]
        [BsonElement("EndTime")]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [DataMember]
        [BsonElement("DateCreated")]
        [Display(Name = "Created Date")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [DataMember]
        [BsonElement("IsComplete")]
        [Display(Name = "Is Complete")]
        public bool IsComplete { get; set; } = false;

        [DataMember]
        [BsonElement("Categories")]
        public List<string> Categories { get; set; }
    }
}
