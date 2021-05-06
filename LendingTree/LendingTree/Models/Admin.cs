using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendingTree.Models
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Admin Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Admin ID required")]
        public string AdminId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}