using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        MyDBEntities db = new MyDBEntities();


        public ActionResult Index()
        {
            return View(db.Products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Photo")]Product obj, HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                byte[] buffer = new byte[photo.ContentLength];
                photo.InputStream.Read(buffer, 0, buffer.Length);
                obj.Photo = buffer;
            }

            db.Products.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            Product obj = db.Products.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(int id, [Bind(Exclude = "Photo")]Product obj, HttpPostedFileBase photo)
        {
            obj.ProductID = id;
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;

            if (photo != null)
            {
                byte[] buffer = new byte[photo.ContentLength];
                photo.InputStream.Read(buffer, 0, buffer.Length);
                obj.Photo = buffer;
            }
            else
            {
                db.Entry(obj).Property("Photo").IsModified = false;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Product obj = db.Products.Find(id);
            db.Products.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetPhoto(int id)
        {
            Product obj = db.Products.Find(id);
            if(obj!=null && obj.Photo != null)
            {
                return File(obj.Photo, "image/jpeg");
            }
            else
            {
                return File(Server.MapPath("~/nophoto.jpg"), "image/jpeg");
            }
        }

    }
}