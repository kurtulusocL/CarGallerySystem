using CarGallerySystem.Data;
using CarGallerySystem.Entities.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarGallerySystem.Controllers
{
    public class ModelController : Controller
    {
        ApplicationDbContext _db;

        public ModelController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var modelList = _db.Models.Include("Cars").Include("Customers").Include("Brand").Where(i => i.IsDeleted == false && i.Cars.Count() > 0).OrderByDescending(i => i.Cars.Count()).ToPagedList(page, 20);
                return View(modelList);
            }
        }

        public ActionResult ModelToCar(int? id, int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var carList = _db.Cars.Include("Category").Include("Brand").Include("Model").Include("Contracts").Include("Customers").Where(i => i.IsAvaible == true && i.IsDeleted == false && i.ModelId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(carList);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Brands = _db.Brands.Where(i => i.IsDeleted == false).OrderByDescending(i => i.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model model)
        {
            if (ModelState.IsValid)
            {
                _db.Models.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Brands = _db.Brands.Where(i => i.IsDeleted == false).OrderByDescending(i => i.Name).ToList();
            using (_db = new ApplicationDbContext())
            {
                var editModel = _db.Models.Find(id);
                if (editModel != null)
                {
                    return View(editModel);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Model model)
        {
            if (ModelState.IsValid)
            {
                _db.Models.Attach(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteModel = _db.Models.Find(id);
                if (deleteModel != null)
                {
                    _db.Models.Remove(deleteModel);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}