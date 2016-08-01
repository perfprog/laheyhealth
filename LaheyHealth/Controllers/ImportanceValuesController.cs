using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaheyHealth.Models;

namespace LaheyHealth.Controllers
{
    public class ImportanceValuesController : Controller
    {
        private SistemContext db = new SistemContext();

        // GET: ImportanceValues
        public ActionResult Index()
        {
            return View(db.ImportanceValues.ToList());
        }

        // GET: ImportanceValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportanceValues importanceValues = db.ImportanceValues.Find(id);
            if (importanceValues == null)
            {
                return HttpNotFound();
            }
            return View(importanceValues);
        }

        // GET: ImportanceValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImportanceValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value,Label,Type")] ImportanceValues importanceValues)
        {
            if (ModelState.IsValid)
            {
                db.ImportanceValues.Add(importanceValues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(importanceValues);
        }

        // GET: ImportanceValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportanceValues importanceValues = db.ImportanceValues.Find(id);
            if (importanceValues == null)
            {
                return HttpNotFound();
            }
            return View(importanceValues);
        }

        // POST: ImportanceValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value,Label,Type")] ImportanceValues importanceValues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importanceValues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importanceValues);
        }

        // GET: ImportanceValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportanceValues importanceValues = db.ImportanceValues.Find(id);
            if (importanceValues == null)
            {
                return HttpNotFound();
            }
            return View(importanceValues);
        }

        // POST: ImportanceValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImportanceValues importanceValues = db.ImportanceValues.Find(id);
            db.ImportanceValues.Remove(importanceValues);
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
