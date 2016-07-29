using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Language
    {
        private int id;
        private String languageName;
        private String culture;

        public String Culture
        {
            get { return culture; }
            set { culture = value; }
        }

        public String LanguageName
        {
            get { return languageName; }
            set { languageName = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}