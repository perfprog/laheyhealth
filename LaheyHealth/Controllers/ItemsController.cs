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
                item = ivm.Item;
                item.Language = lang;
                item.Scale = scale;
                item.Subscale = subScale;
                //Save Changes
                db.Item.Add(item);
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
            if(item.Language!=null)
                ivm.LangId = item.Language.Id;
            if(item.Scale!=null)
                ivm.ScaleId = item.Scale.Id;
            if(item.Subscale!=null)
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
                item.Name = ivm.Item.Name;
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

        //Get Poll
        //This will receive the language in the future, as of now we simply set it inside the controller
        [HttpGet, ActionName("Poll")]
        public ActionResult Poll()
        {
            try {
                if (System.Web.HttpContext.Current.Session["qvm"] == null) {
                    //If poll has not been started yet
                    SistemContext db = new SistemContext();
                    //Get the language that will be used to take the test
                    //For now it will always be english
                    Language lang = db.Language.Find(9);
                    //Create the user that will be taking the test
                    Participant p = new Participant();
                    p.IPAdress = Request.UserHostAddress;
                    p.Language = lang;
                    p.StartDt =DateTime.Now;
                    //Start the poll as not finished, this value will change when the poll is finished
                    p.Finished = false;
                    p.CompleteDt = DateTime.Now;
                    //Save new user to the database
                    db.Participant.Add(p);
                    db.SaveChanges();
                    //Save value of participant id to session variable, will be used throughout the application
                    System.Web.HttpContext.Current.Session["participantId"] = p.Id;
                    //Create new questionViewController, this is what we will use to run through the test
                    QuestionsViewModel qvm = new QuestionsViewModel(p);
                    System.Web.HttpContext.Current.Session["qvm"] = qvm;
                    return View(qvm);
                }
                else
                {
                    //If poll has been started
                    return View((QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"]);
                }
            }
            catch
            {
                Console.WriteLine("Error getting the poll");
            }
            return View();   
        }

        //Post Poll
        //Gets data from ajax call in poll, inserts value and updates the view to show new questions
        [HttpPost, ActionName("Poll")]
        public ActionResult Poll(List<AnswerAux> answers)
        {
            var p = answers;
            //Check if answers list is null and if all items where answered
            QuestionsViewModel m = (QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"];
            if (answers != null && answers.Count() == m.LstItems.Count() && m.allAnswersComplete(answers)) { 
                //Check if all items have a value type

                //Store answers
                foreach (var item in answers)
                {
                    //Create new score to be inserted
                    try
                    {
                            SistemContext dbo = new SistemContext();
                            Scores scoreInsert = new Scores();
                            scoreInsert.ImportanceValues = dbo.ImportanceValues.Find(item.IdSelectedImportance);
                            scoreInsert.SkillValues = dbo.SkillValues.Find(item.IdSelectedSkill);
                            scoreInsert.Item = dbo.Item.Find(item.Id);
                            //We get language, scale and subscale from the item
                            scoreInsert.Language = dbo.Language.Find(scoreInsert.Item.Language.Id);
                            scoreInsert.Scale = dbo.Scale.Find(scoreInsert.Item.Scale.Id);
                            scoreInsert.Subscale = dbo.Subscale.Find(scoreInsert.Item.Subscale.Id);
                            //Get the Participant id from session variable
                            var partIcipantId = System.Web.HttpContext.Current.Session["participantId"];
                            scoreInsert.Participant = dbo.Participant.Find(System.Web.HttpContext.Current.Session["participantId"]);
                            //Before we insert the score we calculate the actual score
                            //To calculate the score we multiply the values for all value types selected
                            scoreInsert.Score = scoreInsert.SkillValues.Value * scoreInsert.ImportanceValues.Value;
                            dbo.Scores.Add(scoreInsert);
                            dbo.SaveChanges();
                            dbo.Dispose();
                    }
                    catch {
                        Console.Write("error inserting values to the database");
                    }
                    //Once the data is inserted into the database we check if the user finished with the poll 
                    QuestionsViewModel qvm = (QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"];
                    qvm.changeSubscale();
                    //Check if the poll is finished
                    var redirectUrl = new UrlHelper(Request.RequestContext).Action("Poll", "Items");
                        return Json(new
                        {
                            error = false,
                            message = "Change to new set of questions"
                        });
                                        
                }
            }
            else {
                return Json(new
                {
                    error = true,
                    message = "All poll options need to be answered before proceeding to the next question"
                });
            }
            return Json("Change page");
            

            //Either reload view with errors or redirect to new view
           
        }
    
    }
}
