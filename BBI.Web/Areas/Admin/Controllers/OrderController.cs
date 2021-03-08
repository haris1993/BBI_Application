using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using BBI.Models.ViewModels;
using BBI.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BBI.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Approve(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusApproved);
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("harisagic19@gmail.com", "haramelo15"),
                    EnableSsl = true
                };

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("harisagic19@gmail.com", "haramelo15");

                smtp.Send("harisagic19@gmail.com", orderFromDb.Email, "BBI Notification", "Zahtjev za vaš paket je odobren za dalje instrukcije posjetite našu obližnju poslovnicu.");

            }
            catch (Exception ex)
            {
            }
            return View(nameof(Index));
        }

        public IActionResult Reject(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusRejected);
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("harisagic19@gmail.com", "haramelo15"),
                    EnableSsl = true
                };

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("harisagic19@gmail.com", "haramelo15");

                smtp.Send("harisagic19@gmail.com", orderFromDb.Email, "BBI Notification", "Zahtjev za vaš paket je odbijen žao nam je. ");

            }
            catch (Exception ex)
            {
            }
            return View(nameof(Index));
        }

        #region API Calls

        public IActionResult GetAllOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll() });
        }

        public IActionResult GetAllPedingOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter:o=>o.Status==SD.StatusSubmitted) });
        }

        public IActionResult GetAllApprovedOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == SD.StatusApproved) });
        }

        #endregion

    }
}
