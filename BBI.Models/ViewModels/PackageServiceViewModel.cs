using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.Models.ViewModels
{
    public class PackageServiceViewModel
    {
        public ServicePackage ServicePackage { get; set; }
        public IEnumerable<SelectListItem> PackageList { get; set; }
    }
}
