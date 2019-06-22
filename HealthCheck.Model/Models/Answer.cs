using System.ComponentModel.DataAnnotations;

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

        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
        public virtual Category Category { get; set; }
        public virtual AnswerOption AnswerOption { get; set; }

        public string GetAnsweredByUser()
        {
            return User.Name;
        }
    }
}