using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Language
    {
        private int id;
        private String languageName;
        private String culture;
        [Required(ErrorMessage = "Culture of language (ex: En-en) is required")]
        public String Culture
        {
            get { return culture; }
            set { culture = value; }
        }
        [Required(ErrorMessage = "Name of language (ex: English) is required")]
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