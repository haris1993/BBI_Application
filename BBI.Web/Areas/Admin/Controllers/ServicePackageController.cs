using BBI.DataAccess.Data.Service.IRepository;
using BBI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BBI.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ServicePackageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public PackageServiceViewModel PackageServiceViewModel { get; set; }

        public ServicePackageController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            PackageServiceViewModel = new PackageServiceViewModel()
            {
                ServicePackage = new BBI.Models.ServicePackage(),
                PackageList = _unitOfWork.Package.GetPackageListForDropDown()
            };
            if(id != null)
            {
                PackageServiceViewModel.ServicePackage = _unitOfWork.ServicePackage.Get(id.GetValueOrDefault());
            }

            return View(PackageServiceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (PackageServiceViewModel.ServicePackage.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\services");
                    var extention = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    PackageServiceViewModel.ServicePackage.ImageUrl = @"\images\services\" + fileName + extention;

                    _unitOfWork.ServicePackage.Add(PackageServiceViewModel.ServicePackage);
                }
                else
                {
                    var serviceFromDb = _unitOfWork.ServicePackage.Get(PackageServiceViewModel.ServicePackage.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        var extention_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        PackageServiceViewModel.ServicePackage.ImageUrl = @"\images\services\" + fileName + extention_new;
                    }
                    else
                    {
                        PackageServiceViewModel.ServicePackage.ImageUrl = serviceFromDb.ImageUrl;
                    }

                    _unitOfWork.ServicePackage.Update(PackageServiceViewModel.ServicePackage);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                PackageServiceViewModel.PackageList = _unitOfWork.Package.GetPackageListForDropDown();
                return View(PackageServiceViewModel);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.ServicePackage.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (serviceFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _unitOfWork.ServicePackage.Remove(serviceFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully." });
        }

        #region API Calls
        public IActionResult GetAllServicePackage()
        {
            return Json(new { data = _unitOfWork.ServicePackage.GetAll(includeProperties: "Package") });
        }
        #endregion
    }
}
