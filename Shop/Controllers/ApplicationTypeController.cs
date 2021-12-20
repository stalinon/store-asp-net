﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop_Models;
using Shop_Utility;
using System.Collections.Generic;
using System.IO;

namespace Shop.Controllers
{
    [Authorize(Roles = WC.AdminRole)]

    public class ApplicationTypeController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ApplicationTypeController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _db.ApplicationType;
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
            _db.ApplicationType.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id < 1)
                return NotFound();
            var obj = _db.ApplicationType.Find(id);
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
                _db.ApplicationType.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 1)
                return NotFound();
            var obj = _db.ApplicationType.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ApplicationType.Find(id);
            if (obj != null)
            {
                foreach(var prod in _db.Product)
                {
                    if (prod != null && prod.CategoryId == id)
                    {
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string upload = webRootPath + WC.ImagePath;

                        var file = Path.Combine(upload, prod.Image);

                        if (System.IO.File.Exists(file))
                        {
                            System.IO.File.Delete(file);
                        }

                        _db.Product.Remove(prod);
                        _db.SaveChanges();
                    }
                }
                _db.ApplicationType.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
