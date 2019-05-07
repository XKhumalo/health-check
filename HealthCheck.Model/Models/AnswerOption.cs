using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthCheck.Model.Models
{
    public class AnswerOption
    {
        [Key]
        public int AnswerOptionId { get; set; }

        public string Option { get; set; }

        public string Description { get; set; }
    }
}
