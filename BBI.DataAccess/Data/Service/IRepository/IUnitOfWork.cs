using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.DataAccess.Data.Service.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryPackageRepository Package { get; }
        IServicePackageRepository ServicePackage { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }

        ISP_Call SP_Call { get; }

        IUserRepository User { get; }
        ICityRepository City { get; }



        void Save();
    }
}
