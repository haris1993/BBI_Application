using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBI.DataAccess.Data.Service.Repository
{
    public class CategoryPackageRepository : Repository<CategoryPackage>, ICategoryPackageRepository
    {
        private ApplicationDbContext _db;

        public CategoryPackageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetPackageListForDropDown()
        {
            return _db.CategoryPackages.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(CategoryPackage package)
        {
            var objFromDb = _db.CategoryPackages.FirstOrDefault(s => s.Id == package.Id);

            objFromDb.Name = package.Name;

            _db.SaveChanges();

        }
    }
}
