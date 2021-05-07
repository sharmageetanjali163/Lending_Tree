using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.WebPages.Html;
using System.Web.Mvc;

namespace LendingTree.Models
{
    public class Agent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Agent Id")]
        [Required]
        public string AgentId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public String LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }

        [Required]
        public String Gender { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public long ContactNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        [System.ComponentModel.DefaultValue(0)]
        public int NoOfApplications { get; set; }

        [Required]
        [Display(Name = "Department")]
        [Range(1, 5)]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [NotMapped]
        public IEnumerable<System.Web.Mvc.SelectListItem> DepartmentList { get; set; }
        
        public ICollection<Loan> Loans { get; set; }
    }
}