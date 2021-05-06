using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LendingTree.Models
{
    public class ForgotPassword
    {
        [Display(Name = "User ID")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "Favourite Song")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Answer Required")]
        public string Ques1 { get; set; }

        [Display(Name = "Favourite Color")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Answer Required")]
        public string Ques2 { get; set; }

        [Display(Name = "Favourite Pet")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Answer Required")]
        public string Ques3 { get; set; }
    }
}