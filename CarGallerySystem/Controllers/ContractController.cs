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
    public class ContractController : Controller
    {
        ApplicationDbContext _db;

        public ContractController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var contractList = _db.Contracts.Include("Category").Include("Customer").Include("Car").Where(i => i.IsDeleted == false).OrderByDescending(i => i.ArrivingTime).ToPagedList(page, 35);
                return View(contractList);
            }
        }

        public ActionResult ContractOff(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var contractList = _db.Contracts.Include("Category").Include("Customer").Include("Car").Where(i => i.IsDeleted == false && i.IsUsing == false).OrderByDescending(i => i.ArrivingTime).ToPagedList(page, 35);
                return View(contractList);
            }
        }

        public ActionResult ContractOn(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var contractList = _db.Contracts.Include("Category").Include("Customer").Include("Car").Where(i => i.IsDeleted == false && i.IsUsing == true).OrderByDescending(i => i.ArrivingTime).ToPagedList(page, 35);
                return View(contractList);
            }
        }

        public ActionResult Detail(int id)
        {
            return View(_db.Contracts.Find(id));
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Cars = _db.Cars.Where(i => i.IsDeleted == false && i.IsAvaible == true).OrderBy(i => i.Plaka).ToList();
            ViewBag.Customers = _db.Customers.Where(i => i.IsDeleted == false).OrderBy(i => i.NameSurname).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contract model)
        {
            if (ModelState.IsValid)
            {
                _db.Contracts.Add(model);
                _db.Entry(model).State = EntityState.Added;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Categories = _db.Categories.Where(i => i.IsDeleted == false).OrderBy(i => i.Name).ToList();
            ViewBag.Cars = _db.Cars.Where(i => i.IsDeleted == false && i.IsAvaible == true).OrderBy(i => i.Plaka).ToList();
            ViewBag.Customers = _db.Customers.Where(i => i.IsDeleted == false).OrderBy(i => i.NameSurname).ToList();
            using (_db = new ApplicationDbContext())
            {
                var editCont = _db.Contracts.Find(id);
                if (editCont != null)
                {
                    return View(editCont);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contract model)
        {
            if (ModelState.IsValid)
            {
                _db.Contracts.Attach(model);
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteCont = _db.Contracts.Find(id);
                if (deleteCont != null)
                {
                    _db.Contracts.Remove(deleteCont);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult SetContractOff(int id)
        {
            var contract = _db.Contracts.Where(i => i.Id == id).SingleOrDefault();
            contract.IsUsing = false;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SetContractOn(int id)
        {
            var contract = _db.Contracts.Where(i => i.Id == id).SingleOrDefault();
            contract.IsUsing = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}