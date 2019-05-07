using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HealthCheck.Model
{
    [DataContract]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Positive { get; set; }

        public string Negative { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("CategoryId")]
        public ICollection<SessionCategory> SessionCategories { get; set; }
    }
}
