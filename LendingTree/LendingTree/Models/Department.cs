using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LendingTree.Models
{
    public class Department
    {
        [Key]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }
    }
}