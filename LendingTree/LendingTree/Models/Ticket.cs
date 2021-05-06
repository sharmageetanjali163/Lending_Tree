using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LendingTree.Models
{
    public class Ticket
    {
        [Key]
        [Display(Name ="Ticket Number")]
        public int RequestId { get; set; }
        
        [Required]
        public string Issue { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [Display(Name ="Date of Ticket")]
        public Nullable<System.DateTime> DateTicket { get; set; }
        
        [Required]
        public string Resolution { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}