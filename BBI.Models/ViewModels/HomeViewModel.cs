using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryPackage> PackageList { get; set; }
        public IEnumerable<ServicePackage> ServicePackageList { get; set; }

    }
}
