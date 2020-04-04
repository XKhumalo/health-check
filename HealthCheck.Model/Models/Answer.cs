using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public class GuestUserAnswer
    {
        [Key]
        public int GuestUserAnswerId { get; set; }

        [Required]
        [ForeignKey("SessionOnlyUser")]
        public int SessionOnlyUserId { get; set; }

        [Required]
        public int SessionId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AnswerOptionId { get; set; }

        public virtual SessionOnlyUser SessionOnlyUser { get; set; }
        public virtual Session Session { get; set; }
        public virtual Category Category { get; set; }
        public virtual AnswerOption AnswerOption { get; set; }

    }
}