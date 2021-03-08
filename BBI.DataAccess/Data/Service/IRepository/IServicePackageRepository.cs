using BBI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.DataAccess.Data.Service.IRepository
{
    public interface IServicePackageRepository : IRepository<ServicePackage>
    {
        void Update(ServicePackage servicePackage);
    }
}
