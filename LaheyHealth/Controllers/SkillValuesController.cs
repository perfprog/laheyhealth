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
    public class SkillValuesController : Controller
    {
        private SistemContext db = new SistemContext();

        // GET: SkillValues
        public ActionResult Index()
        {
            return View(db.SkillValues.ToList());
        }

        // GET: SkillValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillValues skillValues = db.SkillValues.Find(id);
            if (skillValues == null)
            {
                return HttpNotFound();
            }
            return View(skillValues);
        }

        // GET: SkillValues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value,Label,Type")] SkillValues skillValues)
        {
            if (ModelState.IsValid)
            {
                db.SkillValues.Add(skillValues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skillValues);
        }

        // GET: SkillValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillValues skillValues = db.SkillValues.Find(id);
            if (skillValues == null)
            {
                return HttpNotFound();
            }
            return View(skillValues);
        }

        // POST: SkillValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value,Label,Type")] SkillValues skillValues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillValues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skillValues);
        }

        // GET: SkillValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillValues skillValues = db.SkillValues.Find(id);
            if (skillValues == null)
            {
                return HttpNotFound();
            }
            return View(skillValues);
        }

        // POST: SkillValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkillValues skillValues = db.SkillValues.Find(id);
            db.SkillValues.Remove(skillValues);
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
