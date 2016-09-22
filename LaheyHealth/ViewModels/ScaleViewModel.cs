using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaheyHealth.Models;
using System.Web.Mvc;

namespace LaheyHealth.ViewModels
{
    public class ScaleViewModel
    {
        private Scale scale;
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
        
        public Scale Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        //Load Languages to view on object creation
        public ScaleViewModel() {
            this.scale = new Scale();
            LoadLanguages();
        }

        //Load languages onto select list
        public void LoadLanguages() {
            SystemContext db = new SystemContext();
            languageList = new SelectList(db.Language, "Id", "languageName");
        }
        

    }
}