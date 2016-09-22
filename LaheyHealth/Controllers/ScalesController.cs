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
    public class ScalesController : Controller
    {
        private SystemContext db = new SystemContext();

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
        public ActionResult Create(ScaleViewModel scaleViewModel)
        {
            try
            {
                //Get Language
                SystemContext db = new SystemContext();
                Scale scale = new Scale();
                Language lang = db.Language.Find(scaleViewModel.LangId);
                //Asign language to scale
                scale = scaleViewModel.Scale;
                scale.Language = lang;
                var q = db.Scale.Where(m => m.Name.ToUpper() == scaleViewModel.Scale.Name.ToUpper()).SingleOrDefault();
                if (q != null) {
                    ModelState.AddModelError("Error", "This scale name already exists, you can't have two scales with the same name. Please enter a different name.");
                    return View(scaleViewModel);
                }
                //Save scale
                db.Scale.Add(scale);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.WriteLine("Problem storing Scale to database");
                return View(new ScaleViewModel());
            }
        }

        // GET: Scales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var s = db.Scale.Where(m => m.Id == id).SingleOrDefault();
            Scale scale = db.Scale.Find(id);
            if (scale == null)
            {
                return HttpNotFound();
            }
            ScaleViewModel scaleView = new ScaleViewModel();
            scaleView.Scale = scale;
            if(scale.Language!=null)
                scaleView.LangId = scale.Language.Id;
            return View(scaleView);
        }

        // POST: Scales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScaleViewModel scaleViewModel)
        {
            try
            {
                //Get Language
                SystemContext db = new SystemContext();
                //Check if there isn't a scale with the same name
                var q = db.Scale.Where(m => m.Name.ToUpper() == scaleViewModel.Scale.Name.ToUpper()).SingleOrDefault();
                //Make sure you are not comparing against itself
                Scale selfScale = db.Scale.Find(scaleViewModel.Scale.Id);
                if (q != null && selfScale.Name != q.Name)
                {
                    ModelState.AddModelError("Error", "This scale name already exists, you can't have two scales with the same name. Please enter a different name.");
                    return View(scaleViewModel);
                }
                db.Dispose();
                SystemContext dbo = new SystemContext();
                Language lang = dbo.Language.Find(scaleViewModel.LangId);
                Scale scale = dbo.Scale.Find(scaleViewModel.Scale.Id);
                scale.Language = lang;
                scale.Name = scaleViewModel.Scale.Name;
                dbo.SaveChanges();
                dbo.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.WriteLine("Problem storing Scale to database");
            }
            return View(scaleViewModel);
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
