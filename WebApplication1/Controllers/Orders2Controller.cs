using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Orders2Controller : Controller
    {
        MyDBEntities db = new MyDBEntities();
        // GET: Orders2
        public ActionResult Index()
        {
            return View(db.Orders);
        }

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order obj)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
                ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
                return View(obj);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");

            Order obj = db.Orders.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(int id, Order obj)
        {
            if (ModelState.IsValid)
            {
                obj.OrderID = id;
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
                ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
                return View(obj);
            }
        }


    }
}