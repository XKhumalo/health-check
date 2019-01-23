using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Model
{
    public class MongoEntity
    {
        public ObjectId _id { get; set; }
    }
}
