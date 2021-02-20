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
    public class CarController : Controller
    {
        ApplicationDbContext _db;

        public CarController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var carList = _db.Cars.Include("Category").Include("Brand").Include("Model").Include("Contracts").Include("Customers").Where(i => i.IsAvaible == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(carList);
            }
        }

        public ActionResult CarForRent(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var carRentList = _db.Cars.Include("Category").Include("Brand").Include("Model").Include("Contracts").Include("Customers").Where(i => i.IsAvaible == true && i.IsDeleted == false && i.Category.Name == "Kiralık Araçlar").OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(carRentList);
            }
        }

        public ActionResult CarForSell(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var carRentList = _db.Cars.Include("Category").Include("Brand").Include("Model").Include("Contracts").Include("Customers").Where(i => i.IsAvaible == true && i.IsDeleted == false && i.Category.Name == "Satılık Araçlar").OrderByDescending(i => i.CreatedDate).ToPagedList(page, 30);
                return View(carRentList);
            }
        }

        public ActionResult Detail(int id)
        {
            return View(_db.Cars.Find(id));
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Brands = _db.Brands.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Models = _db.Models.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car model)
        {
            if (ModelState.IsValid)
            {
                using (_db=new ApplicationDbContext())
                {
                    _db.Cars.Add(model);
                    _db.Entry(model).State = EntityState.Added;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Brands = _db.Brands.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Models = _db.Models.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            using (_db=new ApplicationDbContext())
            {
                var editCar = _db.Cars.Find(id);
                if (editCar!=null)
                {
                    return View(editCar);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car model)
        {
            if (ModelState.IsValid)
            {
                using (_db=new ApplicationDbContext())
                {
                    _db.Cars.Attach(model);
                    _db.Entry(model).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (_db=new ApplicationDbContext())
            {
                var deleteCar = _db.Cars.Find(id);
                if (deleteCar!=null)
                {
                    _db.Cars.Remove(deleteCar);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult SetAvaibleOff(int id)
        {
            var avaible = _db.Cars.Where(i => i.Id == id).SingleOrDefault();
            avaible.IsAvaible = false;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SetAvaibleOn(int id)
        {
            var avaible = _db.Cars.Where(i => i.Id == id).SingleOrDefault();
            avaible.IsAvaible = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}