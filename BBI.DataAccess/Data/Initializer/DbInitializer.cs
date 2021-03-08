using BBI.Models;
using BBI.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBI.DataAccess.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }



        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            if (_db.Roles.Any(r => r.Name == SD.Admin))
            {
                return;
            }

            var cities = new City[]
            {
                 new City{Name="Mostar"},
                 new City{Name="Sarajevo"},
                 new City{Name="Zenica"},
                 new City{Name="Srebrenica"},
                 new City{Name="Konjic"},
                 new City{Name="Bratunac"}
            };
            foreach (City item in cities)
            {
                _db.Cities.Add(item);
            }

            var categoryPackates = new CategoryPackage[]
           {
                 new CategoryPackage{Name="Basic"},
                 new CategoryPackage{Name="Premium"},
                 new CategoryPackage{Name="Supreme"},
           };
            foreach (CategoryPackage item in categoryPackates)
            {
                _db.CategoryPackages.Add(item);
            }

            var servicePackages = new ServicePackage[]
             {
                     new ServicePackage{Name="Mali paket", Price=5, LongDescription="Veoma zgodan mali paket koji vam omogućava pregršt pogodnosti", ImageUrl=null, PackageId=1},
                     new ServicePackage{Name="Srednji paket", Price=10, LongDescription="Veoma zgodan srednji paket koji vam omogućava pregršt pogodnosti", ImageUrl=null, PackageId=2},
                     new ServicePackage{Name="Velki paket", Price=15, LongDescription="Veoma zgodan velki paket koji vam omogućava pregršt pogodnosti", ImageUrl=null, PackageId=3}
             };

            foreach (ServicePackage item in servicePackages)
            {
                _db.ServicePackages.Add(item);
            }
            


            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Manager)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FullName = "Admin",
                CityId = 1,
            }, "Admin123*").GetAwaiter().GetResult();

            IdentityUser user = _db.Users.Where(u => u.Email == "admin@admin.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult();

        }
    }
}
