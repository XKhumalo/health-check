using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCheck.Model
{
    public class SessionCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionCategoryId { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Session Session { get; set; }
        public Category Category { get; set; }
    }
}
