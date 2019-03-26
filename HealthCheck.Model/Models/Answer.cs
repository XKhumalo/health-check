using HealthCheck.Model.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace HealthCheck.Model
{
    public class Answer : MongoEntity
    {
        [BsonElement("UserId")]
        [Display(Name = "UserId")]
        [BsonRequired]
        [Required]
        public string UserId { get; set; }
        
        [BsonElement("SessionId")]
        [Display(Name = "SessionId")]
        public string SessionId { get; set; }
        
        [BsonElement("CategoryId")]
        [Display(Name = "CategoryId")]
        public string CategoryId { get; set; }
        
        [BsonElement("CategoryChosen")]
        [Display(Name = "CategoryChosen")]
        public AnswerOption CategoryChosen { get; set; }

    }
}
