using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizServices.Models
{
    public partial class Quiz_ContactLog
    {
        public long id { get; set; }
        [Required]
        [StringLength(50)]
        public string EmailFrom { get; set; }
        [Required]
        [StringLength(250)]
        public string EmailSubject { get; set; }
        [Required]
        public string EmailBody { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EmailDate { get; set; }
    }
}
