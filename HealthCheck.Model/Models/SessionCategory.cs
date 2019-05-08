﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCheck.Model
{
    public class SessionCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionCategoryId { get; set; }

        [ForeignKey("Session")]
        [Required]
        public int SessionId { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }

        public Session Session { get; set; }
        public Category Category { get; set; }
    }
}
