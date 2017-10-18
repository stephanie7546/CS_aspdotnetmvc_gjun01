using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {

        MyDBEntities db = new MyDBEntities();

        public ActionResult SearchByName(string name)
        {
            var result =
                from c in db.Customers
                where c.Name.Contains(name)
                select c;
            return View("Index", result);
        }

        public ActionResult SearchByBirthday(DateTime? beginDate, DateTime? endDate)
        {
            if (beginDate == null) beginDate = DateTime.MinValue;
            if (endDate == null) endDate = DateTime.MaxValue;

            var result =
                from c in db.Customers
                where c.Birthday >= beginDate && c.Birthday <= endDate
                select c;
            return View("Index", result);
        }

        public ActionResult Index()
        {
            return View(db.Customers);
        }


        [Authorize(Users="step@hotmail.com")]
        // /Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer obj, HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                obj.Photo = Guid.NewGuid().ToString() + ".jpg";
                string path = Server.MapPath("~/photos/" + obj.Photo);
                photo.SaveAs(path);
            }

            db.Customers.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // /Customers/Edit/3
        public ActionResult Edit(int id)
        {
            Customer obj = db.Customers.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(int id, Customer obj, HttpPostedFileBase photo)
        {
            obj.CustomerID = id;

            if (photo != null)
            {
                if (obj.Photo != null)
                {
                    string pathDelete = Server.MapPath("~/photos/" + obj.Photo);
                    if (System.IO.File.Exists(pathDelete)) System.IO.File.Delete(pathDelete);
                }

                obj.Photo = Guid.NewGuid().ToString() + ".jpg";
                string path = Server.MapPath("~/photos/" + obj.Photo);
                photo.SaveAs(path);
            }


            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // /Customers/Delete/3
        public ActionResult Delete(int id)
        {
            Customer obj = db.Customers.Find(id);

            if (obj.Photo != null)
            {
                string path = Server.MapPath("~/photos/" + obj.Photo);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            db.Customers.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete2(int id)
        {
            Customer obj = db.Customers.Find(id);
            return View(obj);
        }

        [HttpPost]
        [ActionName("Delete2")]
        public ActionResult Delete2Confirm(int id)
        {
            Customer obj = db.Customers.Find(id);

            if(obj.Photo != null)
            {
                string path = Server.MapPath("~/photos/" + obj.Photo);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            db.Customers.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}