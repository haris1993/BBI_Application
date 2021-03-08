using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.Models.ViewModels
{
    public class CartViewModel
    {
        public IList<ServicePackage> ServiceList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public City City { get; set; }
    }
}
