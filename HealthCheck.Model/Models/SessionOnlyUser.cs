using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HealthCheck.Model
{
    //This Model is used for non-authenticated users. Likely external clients or where no AD access available
    [DataContract]
    public class SessionOnlyUser
    {
        public SessionOnlyUser()
        {
            
        }
        public SessionOnlyUser(string name, string sessionKey)
        {
            this.UserName = name;
            this.SessionKey = sessionKey;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionOnlyUserId { get; set; }

        [Required]
        [Display(Name = "Guest User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Session Key")]
        [MaxLength(6, ErrorMessage = "Session Key must be 6 characters")]
        [MinLength(6)]
        public string SessionKey { get; set; }

        [Display(Name = "Session Id")]
        public int SessionId { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime DateCreated { get; set; }

        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; }

        public virtual ICollection<GuestUserAnswer> Answers { get; set; }
    }
}
