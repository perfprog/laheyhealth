using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaheyHealth.Models;
using System.Web.Mvc;

namespace LaheyHealth.ViewModels
{
    public class SubscaleViewModel
    {
        private Subscale subScale;
        private SelectList langList;
        private SelectList scaleList;
        private int langId;
        private int scaleId;
        //Getters and setters
        #region 
        public int ScaleId
        {
            get { return scaleId; }
            set { scaleId = value; }
        }
        
        public int LangId
        {
            get { return langId; }
            set { langId = value; }
        }

        public SelectList ScaleList
        {
            get { return scaleList; }
            set { scaleList = value; }
        }

        public SelectList LangList
        {
            get { return langList; }
            set { langList = value; }
        }

        public Subscale SubScale
        {
            get { return subScale; }
            set { subScale = value; }
        }
        #endregion

        public SubscaleViewModel()
        {
            this.subScale = new Subscale();
            loadData();
        }

        public void loadData() {
            SystemContext db = new SystemContext();
            langList = new SelectList(db.Language,"Id", "languageName");
            scaleList = new SelectList(db.Scale,"Id","name");
        }

    }
}