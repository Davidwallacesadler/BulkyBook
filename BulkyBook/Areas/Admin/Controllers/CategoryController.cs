using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
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