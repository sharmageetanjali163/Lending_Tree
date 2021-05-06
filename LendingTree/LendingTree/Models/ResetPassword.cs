using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LendingTree.Models
{
    public class ResetPassword
    {
        [Display(Name = "Enter New Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "No Password Entered")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Display(Name = "Comfirm Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}