using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using HealthCheck.Common;

namespace HealthCheck.Model
{
    [DataContract]
    public class Session : MongoEntity
    {
        [DataMember]
        [BsonElement("SessionKey")]
        [Display(Name = "Session Key")]
        public string SessionKey { get; set; }

        [DataMember]
        [BsonElement("CreatedBy")]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set;  }

        [DataMember]
        [BsonElement("StartTime")]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataMember]
        [BsonElement("EndTime")]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [DataMember]
        [BsonElement("DateCreated")]
        [Display(Name = "Created Date")]
        public DateTime DateCreated { get; set; }

        [DataMember]
        [BsonElement("IsComplete")]
        [Display(Name = "Is Complete")]
        public bool IsComplete { get; set; }

        [DataMember]
        [BsonElement("IsOpen")]
        [Display(Name = "Is Open")]
        public bool IsOpen { get; set; }

        [DataMember]
        [BsonElement("Categories")]
        public IEnumerable<string> Categories { get; set; }
    }
}
