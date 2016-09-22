using System;
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
    public class ImportanceValuesController : Controller
    {
        private SystemContext db = new SystemContext();

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
            return View(new ImportanceValuesViewModel());
        }

        // POST: ImportanceValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImportanceValuesViewModel importanceViewModel)
        {
            try
            {
                ImportanceValues iv = new ImportanceValues();
                
                iv = importanceViewModel.ImportanceValues;
                SystemContext db = new SystemContext();
                Language lang = db.Language.Find(importanceViewModel.LangId);
                iv.Language = lang;
                if(lang != null)
                {
                    if (lang.LanguageName == "English")
                    {
                        importanceViewModel.ImportanceValues.Type = "Importance to Role";
                    }
                    if(lang.LanguageName == "Español")
                    {
                        importanceViewModel.ImportanceValues.Type = "Importancia para el Rol";
                    }
                }
                
                db.ImportanceValues.Add(iv);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.Write("Error storing new IV");
            }
            return View(importanceViewModel);
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
            ImportanceValuesViewModel ivvm = new ImportanceValuesViewModel();
            ivvm.ImportanceValues = importanceValues;
            if(importanceValues.Language != null)
                ivvm.LangId = importanceValues.Language.Id;
            return View(ivvm);
        }

        // POST: ImportanceValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImportanceValuesViewModel ivvm)
        {
            try {
                //Search for iv we are changing
                SystemContext db = new SystemContext();
                //Search for language changes
                Language lang = db.Language.Find(ivvm.LangId);
                ImportanceValues iv = db.ImportanceValues.Find(ivvm.ImportanceValues.Id);
                //set changes
                iv.Language = lang;
                iv.Label = ivvm.ImportanceValues.Label;
                iv.Value = ivvm.ImportanceValues.Value;
                if(lang != null)
                {
                    if (lang.LanguageName == "English")
                    {
                        iv.Type = "Importance to Role";
                    }
                }
                //save changes
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.WriteLine("Not working");
            }
            return View(ivvm);
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
            db.Dispose();
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
