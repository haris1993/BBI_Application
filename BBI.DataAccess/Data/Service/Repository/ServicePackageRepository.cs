using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBI.DataAccess.Data.Service.Repository
{
    public class ServicePackageRepository : Repository<ServicePackage>, IServicePackageRepository
    {
        private readonly ApplicationDbContext _db;

        public ServicePackageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ServicePackage servicePackage)
        {
            var objFromDb = _db.ServicePackages.FirstOrDefault(s => s.Id == servicePackage.Id);

            objFromDb.Name = servicePackage.Name;
            objFromDb.LongDescription = servicePackage.LongDescription;
            objFromDb.Price = servicePackage.Price;
            objFromDb.ImageUrl = servicePackage.ImageUrl;
            objFromDb.PackageId = servicePackage.PackageId;

            _db.SaveChanges();
        }
    }
}
