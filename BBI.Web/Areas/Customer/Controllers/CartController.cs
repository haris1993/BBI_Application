using BBI.DataAccess.Data.Service.IRepository;
using BBI.DataAccess.Data.Service.Repository;
using BBI.Models;
using BBI.Models.ViewModels;
using BBI.Utility;
using BBI.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BBI.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CartViewModel CartViewModel { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartViewModel = new CartViewModel()
            {
                OrderHeader = new BBI.Models.OrderHeader(),
                ServiceList = new List<ServicePackage>()
            };
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (var serviceId in sessionList)
                {
                    CartViewModel.ServiceList.Add(_unitOfWork.ServicePackage.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Package"));
                }
            }
            return View(CartViewModel);
        }

        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (var serviceId in sessionList)
                {
                    CartViewModel.ServiceList.Add(_unitOfWork.ServicePackage.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Package"));
                    CartViewModel.CityList = _unitOfWork.City.GetCityListForDropDown();
                    CartViewModel.OrderHeader.BirthDate = DateTime.Now;
                }
            }
            return View(CartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                CartViewModel.ServiceList = new List<ServicePackage>();
                foreach (var serviceId in sessionList)
                {
                    CartViewModel.ServiceList.Add(_unitOfWork.ServicePackage.Get(serviceId));
                    CartViewModel.CityList = _unitOfWork.City.GetCityListForDropDown();
                }
            }

            if (!ModelState.IsValid)
            {
                return View(CartViewModel);
            }
            else
            {
                CartViewModel.OrderHeader.OdredDate = DateTime.Now;
                CartViewModel.OrderHeader.Status = SD.StatusSubmitted;
                //CartViewModel.OrderHeader.ServiceCount = CartViewModel.ServiceList.Count;
                _unitOfWork.OrderHeader.Add(CartViewModel.OrderHeader);
                _unitOfWork.Save();

                foreach (var item in CartViewModel.ServiceList)
                {
                    DateTime now = DateTime.Today;
                    int age = now.Year - CartViewModel.OrderHeader.BirthDate.Year;
                    if (CartViewModel.OrderHeader.BirthDate > now.AddYears(-age))
                        age--;

                    double popust = 0;
                    popust += age >= 18 && age <= 25 ? 30 : 0;

                    string city = _unitOfWork.City.GetFirstOrDefault(u => u.Id == CartViewModel.OrderHeader.CityId)?.Name;
                    popust += city != null && city != string.Empty && (city == "Srebrenica" || CartViewModel.OrderHeader.City.Name == "Bratunac" || CartViewModel.OrderHeader.City.Name == "Prijedor") ? 30 : 0;



                    OrderDetails orderDetails = new OrderDetails
                    {
                        ServicePackageId = item.Id,
                        OrderHeaderId = CartViewModel.OrderHeader.Id,
                        ServiceName = item.Name,
                        Price = popust > 0 ? item.Price - ( item.Price * popust / 100) : item.Price
                    };

                    _unitOfWork.OrderDetails.Add(orderDetails);                 

                }                

                _unitOfWork.Save();
                HttpContext.Session.SetObject(SD.SessionCart, new List<int>());
                return RedirectToAction("Details", "Cart", new { id = CartViewModel.OrderHeader.Id });
            }
        }

        public IActionResult Details(int id)
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(id),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id),
                CityList = _unitOfWork.City.GetCityListForDropDown()
            };

            
            return View(orderViewModel);
        }


        public IActionResult OrderConfirmation(int id)
        {

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(id),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id),
                CityList = _unitOfWork.City.GetCityListForDropDown()
            };

            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("harisagic19@gmail.com", "haramelo15"),
                    EnableSsl = true
                };

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("harisagic19@gmail.com", "haramelo15");

                smtp.Send("harisagic19@gmail.com", orderViewModel.OrderHeader.Email, "BBI Notification", "Odrabrani pakat je poslan na uvid u par minuta dok naši operateri pogledaju vaš zahtjev javimo vam se za dalje instrukecije");

            }
            catch (Exception ex)
            {
            }
            return View(id);
        }

        public IActionResult Remove(int serviceId)
        {
            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            sessionList.Remove(serviceId);
            HttpContext.Session.SetObject(SD.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllToTax()
        {
                return Json(new { data = _unitOfWork.ServicePackage.GetAll() });
        }


       
        #endregion
    }
}
