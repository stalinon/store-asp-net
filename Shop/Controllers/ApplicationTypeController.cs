using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop_DataAccess.Repository.IRepository;
using Shop_Models;
using Shop_Utility;

namespace Shop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]

    public class ApplicationTypeController : Controller
    {

        private readonly IApplicationTypeRepository _appRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ApplicationTypeController(IApplicationTypeRepository appRepo, IWebHostEnvironment webHostEnvironment)
        {
            _appRepo = appRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var objList = _appRepo.GetAll();
            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            _appRepo.Add(obj);
            _appRepo.Save();
            return RedirectToAction("Index");
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 1)
                return NotFound();
            var obj = _appRepo.Find(id.GetValueOrDefault());
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appRepo.Update(obj);
                _appRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1)
                return NotFound();
            var obj = _appRepo.Find(id.GetValueOrDefault());
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _appRepo.Find(id.GetValueOrDefault());
            if (obj != null)
            {
                //foreach(var prod in _db.Product)
                //{
                //    if (prod != null && prod.CategoryId == id)
                //    {
                //        string webRootPath = _webHostEnvironment.WebRootPath;
                //        string upload = webRootPath + WC.ImagePath;

                //        var file = Path.Combine(upload, prod.Image);

                //        if (System.IO.File.Exists(file))
                //        {
                //            System.IO.File.Delete(file);
                //        }

                //        _db.Product.Remove(prod);
                //        _db.SaveChanges();
                //    }
                //}
                _appRepo.Remove(obj);
                _appRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
