using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBI.DataAccess.Data.Service.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCityListForDropDown()
        {
                return _db.Cities.Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
        }

        public void Update(City city)
        {
            var objFromDb = _db.Cities.FirstOrDefault(s => s.Id == city.Id);

            objFromDb.Name = city.Name;

            _db.SaveChanges();
        }
    }
}
