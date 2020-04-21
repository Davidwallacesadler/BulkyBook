using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // NOTE: Here we will need a Web host environemnt
        // We need this because we will be uploading images in a folder in the database -- we use this to get the absolute path
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // NOTE: this is the upsert action that get called on HTTPGET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                // Create just a blank product for now since when we are creating and editing are always the dropdowns
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem { 
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                // Create:
                return View(productVM);
            }
            // Edit:
            productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);
        }

        // NOTE: when the user creates or updates in the upsert view we will be using the HttpPost method
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(Product product)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        if (product.Id == 0)
        //        {
        //            _unitOfWork.Product.Add(product);
        //        }
        //        else
        //        {
        //            _unitOfWork.Product.Update(product);
        //        }
        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(product);
        //}

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            // NOTE: including the properties of category and covertype here allow us to access these properties later
            var allObj = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}