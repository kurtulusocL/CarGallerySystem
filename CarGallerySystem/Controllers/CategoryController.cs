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
    public class CategoryController : Controller
    {
        ApplicationDbContext _db;

        public CategoryController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            using (_db = new ApplicationDbContext())
            {
                var cateList = _db.Categories.Include("Cars").Include("Contracts").Where(i => i.IsDeleted == false && i.Cars.Count() > 0).OrderByDescending(i => i.Cars.Count()).ToList();
                return View(cateList);
            }
        }

        public ActionResult CategoryToCar(int? id, int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var carList = _db.Cars.Include("Category").Include("Brand").Include("Model").Include("Contracts").Include("Customers").Where(i => i.IsAvaible == true && i.IsDeleted == false && i.CategoryId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(carList);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.Categories.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var editCate = _db.Categories.Find(id);
                if (editCate != null)
                {
                    return View(editCate);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                using (_db = new ApplicationDbContext())
                {
                    _db.Categories.Attach(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteCate = _db.Categories.Find(id);
                if (deleteCate != null)
                {
                    _db.Categories.Remove(deleteCate);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}