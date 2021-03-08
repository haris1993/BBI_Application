using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BBI.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int ServicePackageId { get; set; }
        [ForeignKey("ServicePackageId")]
        public ServicePackage ServicePackage { get; set; }
        [Required]
        public string ServiceName { get; set; }
        public double Price { get; set; }

    }
}
