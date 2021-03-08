using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Matični broj (JMBG)")]
        [MinLength(13)]
        [MaxLength(13)]
        public string JMBG { get; set; }

        [Required]
        [RegularExpression(@"/^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$/gm")]
        [Display(Name = "Ime i prezime")]

        public string FullName { get; set; }
        [Required]
        [Display(Name = "Spol")]

        public string Gender { get; set; }
        [Required]
        [Display(Name = "Datum rođenja")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Broj mobilnog telefona")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10)]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Grad")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

    }
}
