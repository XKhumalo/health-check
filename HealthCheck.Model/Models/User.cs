﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace HealthCheck.Model.Models
{
    public class User : MongoEntity
    {
        [DataMember]
        [BsonElement("Name")]
        [Display(Name = "Name")]
        [BsonRequired]
        [Required]
        public string Name { get; set; }
    }
}
