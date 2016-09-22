using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Subscale
    {
        private int id;
        private String name;
        private Scale scale;
        private Language language;
        public virtual Language Language
        {
            get { return language; }
            set { language = value; }
        }

        public virtual Scale Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        [Required(ErrorMessage = "Name for Subscale is required")]

        [Display(Name = "Subscale Name")]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}