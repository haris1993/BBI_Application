using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBI.Models
{
    public class WebImage
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public byte[] Picture { get; set; }
    }
}
