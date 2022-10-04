using BulkyBooks1.DataAccessLayer;
using BulkyBooks1.DataAccessLayer.Repository.IRepository;
using BulkyBooks1.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BulkyBookWebProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> objcategories = _unitOfWork.Category.GetAll();
            return View(objcategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(cat);
                _unitOfWork.Save();
                TempData["Success"] = "Created Successfully";
                 return RedirectToAction("Index");
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
           // var edit = _db.Categories.Where(x => x.Id == id).FirstOrDefault();
           var edit = _unitOfWork.Category.GetFirstOrDefault(c => c.Name == "id");
            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Category.Update(cat);
                _unitOfWork.Save();
                TempData["Success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var del = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);   
            return View(del);
        }

        [HttpPost]
        public IActionResult Delete(Category cat)
        {
                _unitOfWork.Category.Remove(cat);
                _unitOfWork.Save();
            TempData["Success"] = "Deleted Successfully";
            return  RedirectToAction("Index");
        }
    }
}
