using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;


namespace LendingTree.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Remote("IsUserExists", "Users", ErrorMessage = "User Id already in use")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public String LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-dd-mm}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [RegularExpression("^[7-9]{1}[0-9]{9}$", ErrorMessage = "Contact Number not valid")]
        public long ContactNumber { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public String Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Display(Name = "Comfirm Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Favourite Song")]
        [Required]
        public string Answer1 { get; set; }

        [Display(Name = "Favourite Color")]
        [Required]
        public string Answer2 { get; set; }

        [Display(Name = "Favourite Pet")]
        [Required]
        public string Answer3 { get; set; }

        public ICollection<Loan> Loans { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        //Need to add security questions
    }
}