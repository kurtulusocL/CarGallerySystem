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
    public class CustomerController : Controller
    {
        ApplicationDbContext _db;

        public CustomerController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var customerList = _db.Customers.Include("Car").Include("Contracts").Include("Brand").Include("Model").Where(i => i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 35);
                return View(customerList);
            }
        }

        public ActionResult Detail(int id)
        {
            return View(_db.Customers.Find(id));
        }

        public ActionResult Create()
        {
            ViewBag.Cars = _db.Cars.Where(i => i.IsAvaible == true && i.IsDeleted == false).OrderBy(i => i.Plaka).ToList();
            ViewBag.Brands = _db.Brands.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Models = _db.Models.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/customer/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Customers.Add(model);
            _db.Entry(model).State = EntityState.Added;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Cars = _db.Cars.Where(i => i.IsAvaible == true && i.IsDeleted == false).OrderBy(i => i.Plaka).ToList();
            ViewBag.Brands = _db.Brands.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Models = _db.Models.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            using (_db = new ApplicationDbContext())
            {
                var editCustomer = _db.Customers.Find(id);
                if (editCustomer != null)
                {
                    return View(editCustomer);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                image.SaveAs(Server.MapPath("~/img/customer/" + image.FileName));
                model.Photo = image.FileName;
            }
            _db.Customers.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteCustomer = _db.Customers.Find(id);
                if (deleteCustomer != null)
                {
                    _db.Customers.Remove(deleteCustomer);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}