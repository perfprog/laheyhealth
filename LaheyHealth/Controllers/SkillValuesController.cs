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
    public class SkillValuesController : Controller
    {
        private SystemContext db = new SystemContext();

        // GET: SkillValues
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.SkillValues.ToList());
        }

        // GET: SkillValues/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new SkillValuesViewModel());
        }

        // POST: SkillValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(SkillValuesViewModel svm)
        {
            try
            {
                //New connection
                SystemContext db = new SystemContext();
                //Find associated language
                Language lang = db.Language.Find(svm.LangId);
                //Create new skill value
                SkillValues sv = new SkillValues();
                //Get data
                sv = svm.SkillValues;
                sv.Language = lang;
                if(lang != null) { 
                    if (lang.LanguageName == "English") { 
                        sv.Type = "Leadership Skill Level";
                    }
                }
                //Store
                db.SkillValues.Add(sv);
                db.SaveChanges();
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Console.Write("Error craeteing Skill Values");
            }
            return View(svm);
        }

        // GET: SkillValues/Edit/5
        [Authorize(Roles = "Admin")]
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
            //Get data onto a Skill Values View 
            SkillValuesViewModel svm = new SkillValuesViewModel();
            svm.SkillValues = skillValues;
            if(skillValues.Language != null)
                svm.LangId = skillValues.Language.Id;
            //return skill values view class to view
            return View(svm);
        }

        // POST: SkillValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(SkillValuesViewModel svm)
        {
            try
            {
                //Create new db connectino
                SystemContext db = new SystemContext();
                //Get skill value related to from database.
                SkillValues sv = db.SkillValues.Find(svm.SkillValues.Id);
                //Get language to be asigned to sv
                Language lang = db.Language.Find(svm.LangId);
                //Assign values to sv
                sv.Label = svm.SkillValues.Label;
                sv.Value = svm.SkillValues.Value;
                sv.Language = lang;
                if (lang != null)
                {
                    if (lang.LanguageName == "English")
                    {
                        sv.Type = "Leadership Skill Level";
                    }
                }
                //Update db
                db.SaveChanges();
                //Close connection
                db.Dispose();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Console.Write("Problem with edition of Skill");
            }
            return View(svm);
        }

        // GET: SkillValues/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
