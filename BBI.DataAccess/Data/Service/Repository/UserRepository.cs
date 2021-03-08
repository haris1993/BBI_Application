using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBI.DataAccess.Data.Service.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void LockUser(string userId)
        {
            var userFormDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            userFormDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnLockUser(string userId)
        {
            var userFormDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            userFormDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
