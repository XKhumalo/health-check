using HealthCheck.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthCheck.Model.Models;

namespace HealthCheck.Model
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int SessionId { get; set; }
        
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AnswerOptionId { get; set; }

        public User User { get; set; }
        public Session Session { get; set; }
        public Category Category { get; set; }

        [ForeignKey("AnswerOptionId")]
        public AnswerOption AnswerOption { get; set; }
        //public AnswerOptions AnswerOptions { get; set; }
    }
}