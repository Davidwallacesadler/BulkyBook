using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    // REMEMBER: Always specify the area!
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // REMEMBER: We used dependency injection in startup to inject the IUnitOfWork Interface:
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // NOTE: Cool way to test if views are linked correctly is to right click on the IActionResult method name and go to view:
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                // Create:
                return View(category);
            }
            // Edit:
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // NOTE: when the user creates or updates in the upsert view we will be using the HttpPost method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            // Checks all the validations in the model
            // We are doing this here on top of the validation scripts within the view as a double layer of authentication
            if(ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                // NOTE: To avoid using magic strings, can use the nameof method:
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // NOTE: We are using dataTables that are loaded with an API
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }
        #endregion
    }
}