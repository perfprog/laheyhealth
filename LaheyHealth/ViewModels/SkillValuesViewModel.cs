using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaheyHealth.Models;
using System.Web.Mvc;

namespace LaheyHealth.ViewModels
{
    public class SkillValuesViewModel
    {
        private SkillValues skillValues;
        private SelectList languageList;
        private int langId;

        public int LangId
        {
            get { return langId; }
            set { langId = value; }
        }


        public SelectList LanguageList
        {
            get { return languageList; }
            set { languageList = value; }
        }

        public SkillValues SkillValues
        {
            get { return skillValues; }
            set { skillValues = value; }
        }

        public SkillValuesViewModel()
        {
            this.skillValues = new SkillValues();
            //Load data for languages
            LoadLanguages();
        }

        public void LoadLanguages()
        {
            SystemContext db = new SystemContext();
            languageList = new SelectList(db.Language, "Id", "languageName");
        }
    }
}