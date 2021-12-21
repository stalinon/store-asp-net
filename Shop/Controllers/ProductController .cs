using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_DataAccess.Data;
using Shop_Models;
using Shop_Models.ViewModels;
using Shop_Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product.Include(u => u.Category).Include(u => u.ApplicationType);

            //foreach (var obj in objList)
            //{
            //    obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            //    obj.ApplicationType = _db.ApplicationType.FirstOrDefault(u => u.Id == obj.ApplicationTypeId);
            //}

            return View(objList);
        }
         
        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productVM.Product.Id == 0)
                {
                    //create
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fs = new FileStream(Path.Combine(upload, fileName + extension),FileMode.Create))
                    {
                        files[0].CopyTo(fs);
                    }

                    productVM.Product.Image = fileName + extension;

                    _db.Product.Add(productVM.Product);
                }
                else
                {
                    //update
                    var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fs = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fs);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }

                    _db.Product.Update(productVM.Product);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            productVM.CategorySelectList = _db.Category.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            productVM.ApplicationTypeSelectList = _db.ApplicationType.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(productVM);
        }

        //GET - UPSERT
        public IActionResult Upsert(int? id)
        {
            //var CategoryDropDown = _db.Category.Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});

            //ViewBag.CategoryDropDown = CategoryDropDown;

            //var product = new Product();

            var productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                ApplicationTypeSelectList = _db.ApplicationType.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            if (id == null)
            {
                // this is for create
                return View(productVM);
            }
            else
            {
                productVM.Product = _db.Product.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1)
                return NotFound();
            //var obj = _db.Product.Find(id);
            var obj = _db.Product.Include(u=>u.Category).Include(u=>u.ApplicationType).FirstOrDefault(u=>u.Id==id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Product.Find(id);

            if (obj != null)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + WC.ImagePath;

                var file = Path.Combine(upload, obj.Image);

                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }

                _db.Product.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
