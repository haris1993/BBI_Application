using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBI.DataAccess.Data.Service.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Package = new CategoryPackageRepository(_db);
            ServicePackage = new ServicePackageRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            User = new UserRepository(_db);
            SP_Call = new SP_Call(_db);
            City = new CityRepository(_db);
        }

        public ICategoryPackageRepository Package { get; private set; }
        public IServicePackageRepository ServicePackage { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IUserRepository User { get; private set; }
        public ISP_Call SP_Call { get; private set; }
        public ICityRepository City { get; private set; }




        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
