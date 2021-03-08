using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models;
using BBI.Utility;
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
    public class CategoryPackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryPackageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CategoryPackage package = new CategoryPackage();
            if (id == null)
            {
                return View(package);
            }
            package = _unitOfWork.Package.Get(id.GetValueOrDefault());
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CategoryPackage package)
        {
            if (ModelState.IsValid)
            {
                if (package.Id == 0)
                {
                    _unitOfWork.Package.Add(package);
                }
                else
                {
                    _unitOfWork.Package.Update(package);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAllPackage()
        {
            return Json(new { data = _unitOfWork.Package.GetAll() });
            //return Json(new { data = _unitOfWork.SP_Call.ReturnList<CategoryPackage>(SD.usp_GetAllCategoryPackage, null) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Package.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _unitOfWork.Package.Remove(objFromDb);
            _unitOfWork.Save();
            return Json((new { success = true, message = "Deleted successfully" }));

        }
        #endregion
    }
}
