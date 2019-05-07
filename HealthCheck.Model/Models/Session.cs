using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HealthCheck.Model
{
    [DataContract]
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [Display(Name = "Session Key")]
        [MaxLength(6, ErrorMessage = "Session Key must be 6 characters")]
        [MinLength(6)]
        public string SessionKey { get; set; }

        [Display(Name = "Created By")]
        public int CreatedById { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Is Complete")]
        public bool IsComplete { get; set; }

        [Display(Name = "Is Open")]
        public bool IsOpen { get; set; }

        [ForeignKey("SessionId")]
        public ICollection<SessionCategory> SessionCategories { get; set; }
    }
}
