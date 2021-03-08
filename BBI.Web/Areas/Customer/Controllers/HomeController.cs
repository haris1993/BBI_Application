using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models.ViewModels;
using BBI.Utility;
using BBI.Web.Extensions;
using BBI.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BBI.Web.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private HomeViewModel HomeViewModel;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeViewModel = new HomeViewModel()
            {
                PackageList = _unitOfWork.Package.GetAll(),
                ServicePackageList = _unitOfWork.ServicePackage.GetAll()
            };

            return View(HomeViewModel);
        }

        public IActionResult Details(int id)
        {
            var serviceFromDb = _unitOfWork.ServicePackage.GetFirstOrDefault(includeProperties: "Package", filter: c => c.Id == id);
            return View(serviceFromDb);
        }

        public IActionResult AddToCart(int serviceId)
        {
            List<int> sessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
            {
                sessionList.Add(serviceId);
                HttpContext.Session.SetObject(SD.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                if (!sessionList.Contains(serviceId))
                {
                    sessionList.Add(serviceId);
                    HttpContext.Session.SetObject(SD.SessionCart, sessionList);
                }
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
