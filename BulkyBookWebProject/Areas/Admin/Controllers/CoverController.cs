using BulkyBooks1.DataAccessLayer.Repository.IRepository;
using BulkyBooks1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWebProject.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class CoverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Cover> objcover = _unitOfWork.Cover.GetAll();
            return View(objcover);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(Cover cat)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Cover.Add(cat);
                _unitOfWork.Save();
                TempData["Success"] = "Created Successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //var edit = _unitOfWork.Cover.Where(x => x.Id == id).FirstOrDefault();
            var edit = _unitOfWork.Cover.GetFirstOrDefault(c => c.Id == id);
            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(Cover cat)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Cover.Update(cat);
                _unitOfWork.Save();
                TempData["Success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var del = _unitOfWork.Cover.GetFirstOrDefault(u => u.Id == id);
            return View(del);
        }

        [HttpPost]
        public IActionResult Delete(Cover cat)
        {
            _unitOfWork.Cover.Remove(cat);
            _unitOfWork.Save();
            TempData["Success"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}

