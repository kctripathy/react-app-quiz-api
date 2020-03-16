using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizServices.Models
{
    public partial class Quiz_PasswordReset
    {
        public long id { get; set; }
        public int GeneratedByUserId { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        public Guid AuthKey { get; set; }
        [StringLength(1)]
        public string Expired_fg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PasswordResetDate { get; set; }
    }
}
