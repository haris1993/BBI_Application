using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBI.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            City city = new City();
            if (id == null)
            {
                return View(city);
            }
            city = _unitOfWork.City.Get(id.GetValueOrDefault());
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(City city)
        {
            if (ModelState.IsValid)
            {
                if (city.Id == 0)
                {
                    _unitOfWork.City.Add(city);
                }
                else
                {
                    _unitOfWork.City.Update(city);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.City.GetAll() });
            //return Json(new { data = _unitOfWork.SP_Call.ReturnList<CategoryPackage>(SD.usp_GetAllCategoryPackage, null) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.City.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _unitOfWork.City.Remove(objFromDb);
            _unitOfWork.Save();
            return Json((new { success = true, message = "Deleted successfully" }));

        }
        #endregion
    }
}
