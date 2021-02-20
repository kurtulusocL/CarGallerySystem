using CarGallerySystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarGallerySystem.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult UserInformation(string userId)
        {
            userId = Convert.ToString(Session["UserID"]);
            using (_db = new ApplicationDbContext())
            {
                var user = _db.Users.Where(i => i.Id == userId).ToList();
                return PartialView("_UserInformation", user);
            }
        }

        public ActionResult CarList()
        {
            using (_db = new ApplicationDbContext())
            {
                var carList = _db.Cars.Include("Category").Include("Brand").Include("Model").Include("Contracts").Include("Customers").Where(i => i.IsAvaible == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(5).ToList();
                return PartialView("_CarList", carList);
            }
        }

        public ActionResult CategoryList()
        {
            using (_db = new ApplicationDbContext())
            {
                var cateList = _db.Categories.Include("Cars").Include("Contracts").Where(i => i.IsDeleted == false && i.Cars.Count() > 0).OrderByDescending(i => i.Cars.Count()).ToList();
                return PartialView("_CategoryList", cateList);
            }
        }

        public ActionResult BrandList()
        {
            using (_db = new ApplicationDbContext())
            {
                var brandList = _db.Brands.Include("Cars").Include("Customers").Include("Models").Where(i => i.IsDeleted == false && i.Cars.Count() > 0).OrderByDescending(i => i.Cars.Count()).Take(9).ToList();
                return PartialView("_BrandList", brandList);
            }
        }

        public ActionResult ModelList()
        {
            using (_db = new ApplicationDbContext())
            {
                var modelList = _db.Models.Include("Cars").Include("Customers").Include("Brand").Where(i => i.IsDeleted == false && i.Cars.Count() > 0).OrderByDescending(i => i.Cars.Count()).Take(9).ToList();
                return PartialView("_ModelList", modelList);
            }
        }

        public ActionResult ContractList()
        {
            using (_db = new ApplicationDbContext())
            {
                var contractList = _db.Contracts.Include("Category").Include("Customer").Include("Car").Where(i => i.IsDeleted == false).OrderByDescending(i => i.ArrivingTime).Take(15).ToList();
                return PartialView("_ContractList", contractList);
            }
        }
    }
}