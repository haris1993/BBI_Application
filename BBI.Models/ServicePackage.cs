using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBI.Models
{
    public class ServicePackage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Service Package Name")]
        public string Name { get; set; }
        public double Price { get; set; }
        [Display(Name ="Description")]
        public string LongDescription { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name ="Image")]
        public string ImageUrl { get; set; }

        [Required]
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public CategoryPackage Package { get; set; }
    }
}
