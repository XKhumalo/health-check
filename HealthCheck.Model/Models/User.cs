using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HealthCheck.Model
{
    [DataContract]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(250, ErrorMessage="Name can only contain 250 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(250, ErrorMessage="Email can only contain 250 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}