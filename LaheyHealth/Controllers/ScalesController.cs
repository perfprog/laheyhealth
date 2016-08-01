﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaheyHealth.Models;
using LaheyHealth.ViewModels;
namespace LaheyHealth.Controllers
{
    public class ScalesController : Controller
    {
        private SistemContext db = new SistemContext();

        // GET: Scales
        public ActionResult Index()
        {
            return View(db.Scale.ToList());
        }

        // GET: Scales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scale scale = db.Scale.Find(id);
            if (scale == null)
            {
                return HttpNotFound();
            }
            return View(scale);
        }

        // GET: Scales/Create
        public ActionResult Create()
        {
            return View(new ScaleViewModel());
        }

        // POST: Scales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Scale scale)
        {
            if (ModelState.IsValid)
            {
                db.Scale.Add(scale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scale);
        }

        // GET: Scales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scale scale = db.Scale.Find(id);
            if (scale == null)
            {
                return HttpNotFound();
            }
            return View(scale);
        }

        // POST: Scales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModels.ScaleViewModel scaleViewModel)
        {
            try
            {
                //Get Language
                SistemContext db = new SistemContext();
                Scale scale = new Scale();
                Language lang = db.Language.Find(scaleViewModel.LangId);
                //Asign language to scale
                scale = scaleViewModel.Scale;
                scale.Language = lang;
                //Save scale
                db.Scale.Add(scale);
                db.SaveChanges();
                return View(scale);
            }
            catch
            {
                Console.WriteLine("Problem storing Scale to database");
            }
            return View();
        }

        // GET: Scales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scale scale = db.Scale.Find(id);
            if (scale == null)
            {
                return HttpNotFound();
            }
            return View(scale);
        }

        // POST: Scales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Scale scale = db.Scale.Find(id);
            db.Scale.Remove(scale);
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
