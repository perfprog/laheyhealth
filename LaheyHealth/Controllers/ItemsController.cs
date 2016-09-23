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
//App used to send messages
using PPIMessagingHelper;
using System.IO;
using System.IO.Compression;
using HiQPdf;

namespace LaheyHealth.Controllers
{
    public class ItemsController : Controller
    {
        private SystemContext db = new SystemContext();

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
                SystemContext db = new SystemContext();
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
                SystemContext db = new SystemContext();
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
                    //Check if poll is finished and redirect to results page if so, makes sure users don't go back and change answers
                    //If poll has not been started yet
                    SystemContext db = new SystemContext();
                    //Get the language that will be used to take the test
                    //For now it will always be english
                    Language lang = db.Language.Find(9);
                    //Create the user that will be taking the test
                    Participant p = new Participant();
                    p.IPAdress = new WebClient().DownloadString("http://icanhazip.com");
                    //if Icanhazip is not working store an empty string in
                    if (p.IPAdress.Equals(""))
                    {
                        p.IPAdress = "Error";
                    }
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
                    //If poll has been started and hasnt been finished
                    QuestionsViewModel qvm = (QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"];
                    if (!qvm.Finished) {
                        return View((QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"]);
                    }
                    else
                    {
                        //If it has been finished direct the user to the results page
                        return RedirectToAction("Results");
                    }
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

                //If user has not already inserted answers for the ones he is answering then:
                //Store answers
                if (!m.alreadyAnswered(answers)) { 
                    foreach (var item in answers)
                    {
                        //Create new score to be inserted
                        try
                        {
                                SystemContext dbo = new SystemContext();
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
                            return Json(new
                            {
                                error = true,
                                message = "System error, please refresh and try again. If problem persists contact questionnaire administrator"
                            });
                        }
                    }
                }
                else {
                    //If the user is updating answers then:
                    foreach (var item in answers)
                    {
                        //Create new score to be inserted
                        try
                        {
                            SystemContext dbo = new SystemContext();
                            Scores scoreInsert = new Scores();
                            scoreInsert.ImportanceValues = dbo.ImportanceValues.Find(item.IdSelectedImportance);
                            scoreInsert.SkillValues = dbo.SkillValues.Find(item.IdSelectedSkill);
                            scoreInsert.Item = dbo.Item.Find(item.Id);
                            //We get language, scale and subscale from the item
                            scoreInsert.Language = dbo.Language.Find(scoreInsert.Item.Language.Id);
                            scoreInsert.Scale = dbo.Scale.Find(scoreInsert.Item.Scale.Id);
                            scoreInsert.Subscale = dbo.Subscale.Find(scoreInsert.Item.Subscale.Id);
                            //Get the Participant id from session variable
                            int partIcipantId = (int)System.Web.HttpContext.Current.Session["participantId"];
                            //Get the score that needs to be updated for the participant
                            Scores s = dbo.Scores.Where(r => r.Participant.Id == partIcipantId && r.Item.Id == scoreInsert.Item.Id).FirstOrDefault();
                            //Before we insert the score we calculate the actual score
                            //To calculate the score we multiply the values for all value types selected
                            s.Score = scoreInsert.SkillValues.Value * scoreInsert.ImportanceValues.Value;
                            s.SkillValues = scoreInsert.SkillValues;
                            s.ImportanceValues = scoreInsert.ImportanceValues;
                            dbo.SaveChanges();
                            dbo.Dispose();
                        }
                        catch
                        {
                            return Json(new
                            {
                                error = true,
                                message = "System error, please refresh and try again. If problem persists contact questionnaire administrator"
                            });
                        }
                    }
                }
                //Check if the poll is finished
                if (m.Finished)
                {
                    //If poll is finished send to results page
                    return Json(new
                    {
                        error = false,
                        message = "Poll Finished"
                    });
                }
                //If poll isn't finished change to new set of questions
                else { 
                    m.changeSubscale();
                
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
                    message = "All questionnaire options need to be answered before proceeding to the next question"
                });
            }
            //Either reload view with errors or redirect to new view
           
        }

        public ActionResult RestartPoll()
        {
            System.Web.HttpContext.Current.Session["qvm"] = null;
            System.Web.HttpContext.Current.Session["participantId"] = null;
            return RedirectToAction("Index", "Home");
        }

        //Post Get
        //Moves backwards in the poll, letts users get the page before the one they are currently on, so they can change answers
        [HttpPost, ActionName("PollBack")]
        public ActionResult PollBack()
        {
            //Check if answers list is null and if all items where answered
            QuestionsViewModel m = (QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"];
            //Check if all items have a value type
            try { 
            //Once the data is inserted into the database we check if the user finished with the poll 
            QuestionsViewModel qvm = (QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"];
            //Move backwards in the poll
            qvm.changeSubscaleBackwards();

            return Json(new
            {
                error = false,
                message = "Change to new set of questions"
            });
            }
            catch
            {
                return Json(new
                {
                    error = true,
                    message = "Server Error, please make sure your internet connection is active, try again or contact system administrator"
                });
            }
        }

        //Send e-mail
        [HttpPost]
        public void SendEmail(string email)
        {
            //Generate report and get link
            string fileLocation = generateResultPDF();

            //Build e-mail and send with link for participants to download
            PPIMessagingHelper.PPIMessaging.PPIMailAddress mail_address = new PPIMessagingHelper.PPIMessaging.PPIMailAddress();
            mail_address.EmailAddress = "noreply@laheyhealth.com";
            mail_address.EmailName = "Lahey Health";
            PPIMessagingHelper.PPIMessaging.PPIMessagingTools.SendEmail("noreply@laheyhealth.com", email, "", "Result from Leading Forward Competency Assessment", "Download your results for Leading Forward Competency Assesment by clicking on the following link: <a href='"+fileLocation+"'>Download Leading Forward Competency Assessment</a>");
        }

        //Get results for the poll
        public ActionResult Results()
        {
            QuestionsViewModel qvm = (QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"];
            if(qvm != null)
            {
                qvm.Finished = true;            
                SystemContext dbo = new SystemContext();
                //Get id of user finishing the poll
                int partIcipantId = (int)System.Web.HttpContext.Current.Session["participantId"];
                //Get top 10 answers by user
                var q = dbo.Scores.Where(m => m.Participant.Id == partIcipantId).OrderByDescending(m => m.Score).Take(10).ToList();
                ResultsViewModel scores = new ResultsViewModel();
                scores.LstScores = q;
                //Update the participant
                SystemContext dbr = new SystemContext();
                Participant r = dbr.Participant.Where(m => m.Id == partIcipantId).FirstOrDefault();
                r.Finished = true;
                r.CompleteDt = DateTime.Now;
                r.Language = r.Language;
                try {
                    dbr.SaveChanges();
                    dbr.Dispose();
                }
                catch (Exception e){
                    Console.WriteLine(e);
                }
                //p.Finished = true;
                //p.CompleteDt = DateTime.Now;
                //dbo.SaveChanges();
                //Return list of scores to the view
                return View(scores);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult resultsToPdf(ViewDataDictionary vd)
        {
            ResultsViewModel scores = (ResultsViewModel)vd.Model;
            //Return list of scores to the view
            return View(scores);
        }

        public string generateResultPDF()
        {
            QuestionsViewModel qvm = (QuestionsViewModel)System.Web.HttpContext.Current.Session["qvm"];
            var FilePath = Server.MapPath("~/Reports");
            string timeStamp = DateTime.Now.ToString("yyyyMMdd");
            Guid fileName = Guid.NewGuid();
            string fName = fileName.ToString() + timeStamp;
            var FilePathZip = System.IO.Path.Combine(FilePath, fName + ".zip");

            ApplicationDbContext db = new ApplicationDbContext();
            //We use the id for user in qvm later on, because of this the model through
            Guid g;
            ViewDataDictionary sendData = new ViewDataDictionary();
            int participantId = qvm.Participant.Id;
            SystemContext dbo = new SystemContext();
            //Get top 10 answers by user
            var q = dbo.Scores.Where(m => m.Participant.Id == participantId).OrderByDescending(m => m.Score).Take(10).ToList();
            ResultsViewModel scores = new ResultsViewModel();
            scores.LstScores = q;
            sendData.Model = scores;
            // get the About view HTML code
            string htmlToConvert = this.RenderViewAsString("resultsToPdf", sendData);
            
            // the base URL to resolve relative images and css
            String thisViewUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
            String baseUrl = thisViewUrl;

            // instantiate the HiQPdf HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            //Set to the highest quality of images possible
            htmlToPdfConverter.Document.ImagesCompression = 0;
            htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Landscape;
            htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
            htmlToPdfConverter.SerialNumber = @"35e2jo+7-uZO2va2+-rabn8e//-7v/s/+bq-6P/s7vHu-7fHm5ubm";
            // render the HTML code as PDF in memory
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

            // send the PDF document to browser
            FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
            g = Guid.NewGuid();
            string gN = g.ToString();
            string name = gN + timeStamp + "laheyQuestionnaire.pdf";
            //fileResult.FileDownloadName = name;
            //Store the file to folder on disc
            MemoryStream ms = new MemoryStream();
            System.IO.File.WriteAllBytes(FilePath + "/" + name, pdfBuffer);
            string fileNameEmail = "";

            fileNameEmail = this.uri(FilePathZip);
            //string requestUrl = this.uri(Request.Url.ToString());
            string requestUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);
            //Store string of file location to be used from e-mail send method
            
            return requestUrl + "/Reports/" + fileNameEmail;
        }


        public string RenderViewAsString(string viewName, ViewDataDictionary viewData)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    viewData,
                    new TempDataDictionary(),
                    stringWriter
                    );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }

        //Method used to produce URI (call when you want to return a specific string for a file location)
        private string uri(string s)
        {
            string result = "";
            Uri uri = new Uri(s);
            if (uri.IsFile)
            {
                result = System.IO.Path.GetFileName(uri.LocalPath);
            }
            return result;
        }

        //Start poll, makes session null and starts new poll
        public ActionResult startPoll()
        {
            System.Web.HttpContext.Current.Session["qvm"] = null;
            return RedirectToAction("Poll");
        }
    }
}
