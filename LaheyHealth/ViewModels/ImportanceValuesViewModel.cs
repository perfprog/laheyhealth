using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaheyHealth.Models;
using System.Web.Mvc;

namespace LaheyHealth.ViewModels
{
    public class ImportanceValuesViewModel
    {
        private ImportanceValues importanceValues;
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
        
        public ImportanceValues ImportanceValues
        {
            get { return importanceValues; }
            set { importanceValues = value; }
        }

        public ImportanceValuesViewModel()
        {
            this.importanceValues = new ImportanceValues();
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