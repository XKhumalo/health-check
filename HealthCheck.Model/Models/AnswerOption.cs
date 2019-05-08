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

        [Required]
        public string Option { get; set; }

        [Required]
        public string Description { get; set; }
    }
}