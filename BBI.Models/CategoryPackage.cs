using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBI.Models
{
    public class CategoryPackage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Package Name")]
        public string Name { get; set; }
    }
}
