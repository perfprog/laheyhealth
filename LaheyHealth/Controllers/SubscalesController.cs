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
    public class SubscalesController : Controller
    {
        private SystemContext db = new SystemContext();

        // GET: Subscales
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Subscale.ToList());
        }

        // GET: Subscales/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscale subscale = db.Subscale.Find(id);
            if (subscale == null)
            {
                return HttpNotFound();
            }
            return View(subscale);
        }

        // GET: Subscales/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new SubscaleViewModel());
        }

        // POST: Subscales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(SubscaleViewModel svm)
        {
            try
            {
                //Craete new connection
                SystemContext db = new SystemContext();
                //Get scale and language
                Language lang = db.Language.Find(svm.LangId);
                Scale scale = db.Scale.Find(svm.ScaleId);
                //Create new subscale and assign values to it
                Subscale subScale = new Subscale();
                subScale = svm.SubScale;
                subScale.Scale = scale;
                subScale.Language = lang;
                //Store data and close connection
                db.Subscale.Add(subScale);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Console.Write("Error creating subscale");
            }
            return View(svm);
        }

        // GET: Subscales/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscale subscale = db.Subscale.Find(id);
            if (subscale == null)
            {
                return HttpNotFound();
            }
            //Assign values to subscale view model
            SubscaleViewModel svm = new SubscaleViewModel();
            svm.SubScale = subscale;
            if(subscale.Language!=null)
                svm.LangId = subscale.Language.Id;
            if(subscale.Scale!=null)
                svm.ScaleId = subscale.Scale.Id;
            return View(svm);
        }

        // POST: Subscales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(SubscaleViewModel svm)
        {
            try
            {
                //Create new context connection
                SystemContext db = new SystemContext();
                //Get susbscale to be edited as well as scale and language
                Subscale subscale = db.Subscale.Find(svm.SubScale.Id);
                Language lang = db.Language.Find(svm.LangId);
                Scale scale = db.Scale.Find(svm.ScaleId);
                //Asign new data to subscale
                subscale.Language = lang;
                subscale.Scale = scale;
                subscale.Name = svm.SubScale.Name;
                //Save changes and dispose connection
                
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Console.Write("Edition of subscale is not working");
            }

            return View(svm);
        }

        // GET: Subscales/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscale subscale = db.Subscale.Find(id);
            if (subscale == null)
            {
                return HttpNotFound();
            }
            return View(subscale);
        }

        // POST: Subscales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscale subscale = db.Subscale.Find(id);
            db.Subscale.Remove(subscale);
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
