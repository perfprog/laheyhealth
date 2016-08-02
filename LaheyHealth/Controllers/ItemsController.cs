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
    public class ItemsController : Controller
    {
        private SistemContext db = new SistemContext();

        // GET: Items
        public ActionResult Index()
        {
            return View(db.Item.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View(new ItemViewModel());
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel ivm)
        {
            try
            {
                //Open context
                SistemContext db = new SistemContext();
                //Find Language
                Language lang = db.Language.Find(ivm.LangId);
                //Find Scale
                Scale scale = db.Scale.Find(ivm.ScaleId);
                //Find Subscale
                Subscale subScale = db.Subscale.Find(ivm.SubScaleId);
                //New Item
                Item item = new Item();
                //Assign values
                item.Language = lang;
                item.Scale = scale;
                item.Subscale = subScale;
                //Save Changes
                db.SaveChanges();
                //Dispose conection
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.Write("Problem storing new Item");
            }
            return View(ivm);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            
            if (item == null)
            {
                return HttpNotFound();
            }
            //Create item view
            ItemViewModel ivm = new ItemViewModel();
            //Assign values
            ivm.Item = item;
            ivm.LangId = item.Language.Id;
            ivm.ScaleId = item.Scale.Id;
            ivm.SubScaleId = item.Subscale.Id;
            db.Dispose();
            //Return item view model to view
            return View(ivm);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemViewModel ivm)
        {
            try
            {
                //Open new connection
                SistemContext db = new SistemContext();
                //Get item that is being updated
                Item item = db.Item.Find(ivm.Item.Id);
                //Get language being assigned
                Language lang = db.Language.Find(ivm.LangId);
                //Get scale being assigned
                Scale scale = db.Scale.Find(ivm.ScaleId);
                //Get subscale being assigned
                Subscale subScale = db.Subscale.Find(ivm.SubScaleId);
                //Assign new values to item
                item = ivm.Item;
                item.Language = lang;
                item.Scale = scale;
                item.Subscale = subScale;
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.Write("Error editing Item");
            }
            return View(ivm);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Item.Find(id);
            db.Item.Remove(item);
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
