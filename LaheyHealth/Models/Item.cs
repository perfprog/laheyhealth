using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Item
    {
        private int id;
        
        private String name;
        private Scale scale;
        
        private Language language;
        
        private Subscale subscale;

        [Required(ErrorMessage = "Subscale it pertains to is required")]
        public Subscale Subscale
        {
            get { return subscale; }
            set { subscale = value; }
        }

        [Required(ErrorMessage = "Language of item is required")]
        public Language Language
        {
            get { return language; }
            set { language = value; }
        }
        [Required(ErrorMessage = "Scale it pertains to is required")]
        public Scale Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        [Required(ErrorMessage = "Name (text of question) is required")]
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