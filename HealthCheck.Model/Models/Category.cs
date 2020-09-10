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
        [StringLength(100, ErrorMessage="Name can only contain 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage="Description can only contain 500 characters")]
        public string Description { get; set; }

        [Required]
        [StringLength(500, ErrorMessage="Positive description can only contain 500 characters")]
        public string Positive { get; set; }

        [Required]
        [StringLength(500, ErrorMessage="Negative description can only contain 500 characters")]
        public string Negative { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<SessionCategory> SessionCategories { get; set; }

        public virtual Answer Answer { get; set; }
        //public virtual GuestUserAnswer GuestUserAnswer { get; set; }
    }
}
