using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace HealthCheck.Model
{
    [DataContract]
    public class MongoEntity
    {
        [DataMember]
        [BsonRequired]
        [BsonId]
        [Required]
        public ObjectId _id { get; set; }
    }
}
