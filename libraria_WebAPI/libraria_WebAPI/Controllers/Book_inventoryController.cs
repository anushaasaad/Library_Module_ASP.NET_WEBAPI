using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using libraria_WebAPI.Models;

namespace libraria_WebAPI.Controllers
{
    public class Book_inventoryController : Controller
    {
        private librariaEntities4 db = new librariaEntities4();

        // GET: Book_inventory
        public ActionResult Index()
        {
            return View(db.Book_inventory.ToList());
        }

        // GET: Book_inventory/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_inventory book_inventory = db.Book_inventory.Find(id);
            if (book_inventory == null)
            {
                return HttpNotFound();
            }
            return View(book_inventory);
        }

        // GET: Book_inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book_inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "book_id,book_name,genre,edition,actual_stock,language,book_cost,pages,description,current_stock,img_link,author_name")] Book_inventory book_inventory)
        {
            if (ModelState.IsValid)
            {
                db.Book_inventory.Add(book_inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book_inventory);
        }

        // GET: Book_inventory/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_inventory book_inventory = db.Book_inventory.Find(id);
            if (book_inventory == null)
            {
                return HttpNotFound();
            }
            return View(book_inventory);
        }

        // POST: Book_inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "book_id,book_name,genre,edition,actual_stock,language,book_cost,pages,description,current_stock,img_link,author_name")] Book_inventory book_inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book_inventory);
        }

        // GET: Book_inventory/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_inventory book_inventory = db.Book_inventory.Find(id);
            if (book_inventory == null)
            {
                return HttpNotFound();
            }
            return View(book_inventory);
        }

        // POST: Book_inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Book_inventory book_inventory = db.Book_inventory.Find(id);
            db.Book_inventory.Remove(book_inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
