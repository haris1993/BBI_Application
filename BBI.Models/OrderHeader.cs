using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBI.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Niste unijeli JMBG")]
        [Display(Name = "Matični broj (JMBG)")]
        [MinLength(13)]
        [MaxLength(13)]
        public string JMBG { get; set; }

        [Required(ErrorMessage ="Niste unijeli ispravno ime i prezime")]
        //[RegularExpression(@"/^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$/gm")]
        //[RegularExpression(@"^([A-z][A-Za-z]*\s*[A-Za-z]*)$")]
        [Display(Name = "Ime i prezime")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Niste unijeli ispravno spol")]
        [Display(Name = "Spol")]

        public string Gender { get; set; }
        [Required(ErrorMessage = "Niste unijeli ispravno datum rođenja")]
        [Range(typeof(DateTime), "1/1/1900", "01/01/2021", ErrorMessage = "Datum rođenja mora biti između {1} and {2}")]
        [Display(Name = "Datum rođenja")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Niste unijeli ispravno broj telefona")]
        [Display(Name = "Broj mobilnog telefona")]
        [MinLength(9)]
        [MaxLength(12)]
        //[RegularExpression(@"^([\+]?(?:00)?[0-9]{1,3}[\s.-]?[0-9]{1,12})([\s.-]?[0-9]{1,4}?)$/gm")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Niste unijeli ispravno email adresu")]
        [Display(Name = "Email adresa")]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^([\w\.\-] +)@([\w\-] +)((\.(\w){2, 3})+)$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Niste izabrali grad")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        //[Required(ErrorMessage = "Niste izabrali grad")]
        public City City { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public DateTime OdredDate { get; set; }
    }
}
