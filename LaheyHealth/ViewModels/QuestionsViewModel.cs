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
        //Skill values to be asked
        private List<SkillValues>  lstSkill = new List<SkillValues>();
        
        //Importance values to be asked
        private List<ImportanceValues> lstImportance = new List<ImportanceValues>();

        //Stores type for Skill Values to show on view (gets updated depending on language)
        private string skillType;
        //Stores type for Importance Values to show on view (gets updated depending on language)
        private string importanceType;

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
            SistemContext db = new SistemContext();
            var items = db.Item.Where(m => m.Language.Id == lang.Id).OrderBy(m=> m.Scale.Id).ToList();
            lstItems = items;
            totalAmmount = items.Count();
            currentQuestion = 0;
            //Set the current scale the first one that we are going to work in
            currentScale = lstItems[CurrentQuestion].Scale.Name;
            //Select Skill lists  that users need to answer for the language that was selected
            lstSkill = db.SkillValues.Where(m => m.Language.Id == lang.Id).OrderBy(m => m.Id).ToList();
            //Selec importance lists that users need to answe for the language that was selected
            lstImportance = db.ImportanceValues.Where(m => m.Language.Id == lang.Id).OrderBy(m => m.Id).ToList();

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

        }

        /*
        
         var q = db.Scale.Where(m => m.Name.ToUpper() == scaleViewModel.Scale.Name.ToUpper()).SingleOrDefault();
        */

    }
}