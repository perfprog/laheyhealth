using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaheyHealth.Models;
using System.Web.Mvc;

namespace LaheyHealth.ViewModels
{
    public class ItemViewModel
    {
        private Item item;
        private SelectList langList;
        private int langId;
        private SelectList scaleList;
        private int scaleId;
        private SelectList subScaleList;
        private int subScaleId;

        //Getters and setters
        #region
        
        public int SubScaleId
        {
            get { return subScaleId; }
            set { subScaleId = value; }
        }

        public SelectList SubScaleList
        {
            get { return subScaleList; }
            set { subScaleList = value; }
        }

        public int ScaleId
        {
            get { return scaleId; }
            set { scaleId = value; }
        }

        public SelectList ScaleList
        {
            get { return scaleList; }
            set { scaleList = value; }
        }

        public int LangId
        {
            get { return langId; }
            set { langId = value; }
        }



        public SelectList LangList
        {
            get { return langList; }
            set { langList = value; }
        }
        
        public Item Item
        {
            get { return item; }
            set { item = value; }
        }
        #endregion

        public ItemViewModel()
        {
            this.item = new Item();
            loadLists();
        }
        public void loadLists()
        {
            SystemContext db = new SystemContext();
            this.langList = new SelectList(db.Language, "Id", "LanguageName");
            this.scaleList = new SelectList(db.Scale, "Id", "name");
            this.subScaleList = new SelectList(db.Subscale,"Id", "name");
        }

    }
}