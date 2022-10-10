using BulkyBooks1.DataAccessLayer.Repository;
using BulkyBooks1.DataAccessLayer.Repository.IRepository;
using BulkyBooks1.Models;
using BulkyBooks1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Helpers;

namespace BulkyBookWebProject.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //IEnumerable<Product> objcover = _unitOfWork.Product.GetAll();
            return View();
        }


        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }),
                CoverList = _unitOfWork.Cover.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }),
             };

            if (id == null || id == 0)
            {
                //create product

                return View(productViewModel);
            }
            else
            {
                productViewModel.Product= _unitOfWork.Product.GetFirstOrDefault(u=>u.Id==id);
                return View(productViewModel);
                //update product
            }

           
        }
    
        
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel cat, IFormFile? file )
        {
        
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if (cat.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, cat.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    cat.Product.ImageUrl = @"\images\Products" + fileName + extension;

                }
                if (cat.Product.Id == 0)
                {
                _unitOfWork.Product.Add(cat.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(cat.Product);
                }
                _unitOfWork.Save();
                TempData["Success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            return View(cat);
        }


        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(incluideproperties: "Category,Cover");
            return Json(new { data = productList });
            //return Ok(productList);
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }


            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath , obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
          
        }
        #endregion

    }


}

