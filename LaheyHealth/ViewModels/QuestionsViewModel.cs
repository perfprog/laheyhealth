using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaheyHealth.Models;

namespace LaheyHealth.ViewModels
{
    //The questions view model holds all data for participant as well as questions
    //Is used to insert values for questions into the database and keep track of what questions we are currently on
    public class QuestionsViewModel
    {
        //Used to the index of the question we are currently on
        private int currentQuestion;
        //Used to keep track of the amount of questions that are in the poll
        private int totalAmmount;
        //Stores all items that will be asked
        private List<Item> lstItems = new List<Item>();
        //Participant that is taking the poll
        private Participant participant;
        //Stores index of item answer last inserted into the db
        private int lastInsertedAnswer;
        //Stores current scale, needs to be changed
        private string currentScale;
        //Stores name of current subscale
        public string currentSubscale { get; set; }
        //Current Subscale index we use this to know how many subscales need to be answered to finish up the poll
        public int currentSubscaleIndex { get; set; }
        //Skill values to be asked
        private List<SkillValues>  lstSkill = new List<SkillValues>();
        //Importance values to be asked
        private List<ImportanceValues> lstImportance = new List<ImportanceValues>();
        //Stores type for Skill Values to show on view (gets updated depending on language)
        private string skillType;
        //Stores type for Importance Values to show on view (gets updated depending on language)
        private string importanceType;
        //Stores a boolean that informs us if the poll has been finished or not
        //Is used to show different button on view so users can go ahead and go to the results page
        private bool finished;
        //skills selected - used if answers have already been set for the element
        public List<int> lstSkillValueAnswers { get; set; }
        //importance values selected - used if answers have already been set for the element
        public List<int> lstImportanceValueAnswers { get; set; }
        //store information letting us know we are in the last page
        public bool lastPage { get; set; }
        

        private List<Subscale> lstSubscale = new List<Subscale>();

        public List<Subscale> LstSubscale
        {
            get { return lstSubscale; }
            set { lstSubscale = value; }
        }


        public int currentScaleInt { get; set; }

        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }


        public string ImportanceType
        {
            get { return importanceType; }
            set { importanceType = value; }
        }


        public string SkillType
        {
            get { return skillType; }
            set { skillType = value; }
        }



        //Getters and setters
        #region
        public List<SkillValues> LstSkill
        {
            get { return lstSkill; }
            set { lstSkill = value; }
        }
        public List<ImportanceValues> LstImportance
        {
            get { return lstImportance; }
            set { lstImportance = value; }
        }
        public string CurrentScale
        {
            get { return currentScale; }
            set { currentScale = value; }
        }
        public int LastInsertedAnswer
        {
            get { return lastInsertedAnswer; }
            set { lastInsertedAnswer = value; }
        }
        public Participant Participant
        {
            get { return participant; }
            set { participant = value; }
        }

        public List<Item> LstItems
        {
            get { return lstItems; }
            set { lstItems = value; }
        }


        public int TotalAmmounQuestions
        {
            get { return totalAmmount; }
            set { totalAmmount = value; }
        }

        public int CurrentQuestion
        {
            get { return currentQuestion; }
            set { currentQuestion = value; }
        }
        #endregion 

        public QuestionsViewModel(Participant participant)
        {
            this.participant = participant;
            LoadData(participant.Language);
        }

        //Receives language and searches for values for questions in those languages
        //Items are loaded ordered by scale number
        public void LoadData(Language lang)
        {
            this.lastPage = false;
            SystemContext db = new SystemContext();
            this.lstImportanceValueAnswers = new List<int>();
            this.lstSkillValueAnswers = new List<int>();
            //We will get items in a different manner
            //var items = db.Item.Where(m => m.Language.Id == lang.Id).OrderBy(m=> m.Scale.Id).ToList();
            //Get subscales asociated to the language and order them by scale
            var lstSubscales = db.Subscale.Where(m => m.Language.Id == lang.Id).OrderBy(m => m.Scale.Id).ToList();
            //Store lst of subscale that we will be working on
            this.LstSubscale = lstSubscales;
            //Get the ammount of subscales that need to be answered
            totalAmmount = this.LstSubscale.Count();
            //Set the first items to be answered (we will be getting them by subscale id)
            int subScaleId = LstSubscale[0].Id;
            this.currentSubscaleIndex = 0;
            var items = db.Item.Where(m => m.Subscale.Id == subScaleId).ToList();
            LstItems = items;
            currentScaleInt = 0;
            //Set the current scale the first one that we are going to work in
            currentScale = LstItems[currentScaleInt].Scale.Name;
            currentSubscale = LstItems[currentScaleInt].Subscale.Name;
            //Select Skill lists  that users need to answer for the language that was selected
            lstSkill = db.SkillValues.Where(m => m.Language.Id == lang.Id).OrderBy(m => m.Value).ToList();
            //Selec importance lists that users need to answe for the language that was selected
            lstImportance = db.ImportanceValues.Where(m => m.Language.Id == lang.Id).OrderBy(m => m.Value).ToList();

            //If lstSkill is not empty we set the value for its type so we can show it on the view
            //Done this way to automatically get language
            if (lstSkill.Count > 0)
                skillType = lstSkill[0].Type;
            //if lstImportance is not empty we set the value for its type so we can show it on the view
            //Done this way to automatically get language
            if(lstImportance.Count > 0)
            {
                importanceType = lstImportance[0].Type;
            }
            this.finished = false;
        }

        //Change subscale we are working on
        internal void changeSubscale()
        {
            //Add value to index of current scale
            this.currentSubscaleIndex++;
            //If land = to length then we are on the last page of the poll and have to show View Results button
            //We set last page equals to true
            if (currentSubscaleIndex == LstSubscale.Count() - 1) {
                this.lastPage = true;
            }
            //Check if poll is finished and change value to finished
            if (currentSubscaleIndex == LstSubscale.Count())
                this.finished = true;
            //If the poll is not finished update items that will be shown
            if (!this.finished)
            {
                this.updateItems();
            }
        }
        //Update current scale and items
        internal void updateItems()
        {
            this.lstImportanceValueAnswers.Clear();
            this.lstSkillValueAnswers.Clear();
            SystemContext dbo = new SystemContext();
            //Get subscale we are working on
            int subscaleInt = this.lstSubscale[this.currentSubscaleIndex].Id;
            //Get items associated to this scale
            var q = dbo.Item.Where(item => item.Subscale.Id == subscaleInt).ToList();
            this.lstItems = q;
            //Get all answers inserted previously for these scales
            int participantId = (int)System.Web.HttpContext.Current.Session["participantId"];
            foreach (Item i in this.LstItems)
            {
                Scores s = dbo.Scores.Where(m => m.Item.Id == i.Id && m.Participant.Id == participantId).FirstOrDefault();
                if(s != null) { 
                    this.lstImportanceValueAnswers.Add(s.ImportanceValues.Id);
                    this.lstSkillValueAnswers.Add(s.SkillValues.Id);
                }
            }
            //Set the current scale to be shown on the view
            this.currentScale = lstItems[0].Scale.Name;
            this.currentSubscale = LstItems[0].Subscale.Name;
            System.Web.HttpContext.Current.Session["qvm"] = this;
        }
        //Go back one subscale
        public void changeSubscaleBackwards()
        {
            //Add value to index of current scale
            if (this.currentSubscaleIndex != 0) { 
                this.currentSubscaleIndex--;
                this.lastPage = false;
                //If the poll is not finished update items that will be shown
                if (!(currentSubscaleIndex > LstSubscale.Count()))
                {
                    this.updateItems();
                }
            }
        }

        //Since page numbers are binded to current scale we send back current scale +1 for page number (index is based of 0)
        public int get_page_number() {
            return this.currentSubscaleIndex + 1;
        }

        internal bool alreadyAnswered(List<AnswerAux> answers)
        {
            //Check if the questions have been answered (if one of the questions in the list is answered then they all are)

            bool answered = false;
            SystemContext db = new SystemContext();
            var item = answers[0];
            try
            {
                Item auxItem = db.Item.Find(item.Id);
                //Search the database to check if there us one answer with this id and user id
                int userId = (int)System.Web.HttpContext.Current.Session["participantId"];
                Scores result = null;
                result = db.Scores.Where(m => m.Participant.Id == userId && m.Item.Id == auxItem.Id).FirstOrDefault();
                if(result != null)
                {
                    answered = true;
                }
            }
            catch
            {
                Console.WriteLine("Error comparing items to answers");
                return false;
            }
            return answered;
        }

        public int get_total_pages()
        {
            return this.lstSubscale.Count();
        }

        //Checks if all answers inserted are completed
        internal bool allAnswersComplete(List<AnswerAux> answers)
        {
            foreach (var item in answers)
            {
                if (!item.AllAssigned)
                    return false;
            }
            return true;
        }

        public double getPercetageOfQuestionsLeft()
        {
            int total = this.get_total_pages();
            int actual = this.get_page_number();
            double a = actual * 75;
            a = a / total;
            a = a + 25;
            return a;
        }

    }
}