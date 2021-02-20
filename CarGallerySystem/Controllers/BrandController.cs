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
    public class BrandController : Controller
    {
        ApplicationDbContext _db;

        public BrandController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var brandList = _db.Brands.Include("Cars").Include("Customers").Include("Models").Where(i => i.IsDeleted == false && i.Cars.Count() > 0).OrderByDescending(i => i.Cars.Count()).ToPagedList(page, 20);
                return View(brandList);
            }
        }

        public ActionResult BrandToCar(int? id, int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var carList = _db.Cars.Include("Category").Include("Brand").Include("Model").Include("Contracts").Include("Customers").Where(i => i.IsAvaible == true && i.IsDeleted == false && i.BrandId == id).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(carList);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/brand/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Brands.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var editBrand = _db.Brands.Find(id);
                if (editBrand != null)
                {
                    return View(editBrand);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/brand/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Brands.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteBrand = _db.Brands.Find(id);
                if (deleteBrand != null)
                {
                    _db.Brands.Remove(deleteBrand);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}