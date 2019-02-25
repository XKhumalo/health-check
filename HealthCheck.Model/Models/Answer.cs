using HealthCheck.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Model
{
    public class Answer : MongoEntity
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string CategoryId { get; set; }
        public AnswerOption CategoryChosen { get; set; }

    }
}
