using CarGallerySystem.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarGallerySystem.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _db;

        public AdminController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminList(int page = 1)
        {
            using (_db = new ApplicationDbContext())
            {
                var adminList = _db.Users.Where(i => i.Id != null).OrderBy(i => i.NameLastname).ToPagedList(page, 15);
                return View(adminList);
            }
        }

        public ActionResult Delete(string id)
        {
            using (_db = new ApplicationDbContext())
            {
                var deleteAdmin = _db.Users.Find(id);
                if (deleteAdmin != null)
                {
                    _db.Users.Remove(deleteAdmin);
                    _db.SaveChanges();
                }
                return RedirectToAction("AdminList");
            }
        }
    }
}