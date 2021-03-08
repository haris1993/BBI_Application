using BBI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.DataAccess.Data.Service.IRepository
{
    public interface ICategoryPackageRepository : IRepository<CategoryPackage>
    {
        IEnumerable<SelectListItem> GetPackageListForDropDown();

        void Update(CategoryPackage package);
    }
}
