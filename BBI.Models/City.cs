using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBI.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Grad")]
        public string Name { get; set; }
        [Display(Name = "Porez(%)")]
        public double Tax { get; set; } = 0;
    }
}
