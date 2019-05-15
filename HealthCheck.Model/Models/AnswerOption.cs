using System.ComponentModel.DataAnnotations;

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

        public virtual Answer Answer { get; set; }
    }
}