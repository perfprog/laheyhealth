using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Scale
    {
        private int id;
        private String name;
        private Language language;
        
        
        public virtual Language Language
        {
            get { return language; }
            set { language = value; }
        }
        [Required(ErrorMessage = "Name of Scale is required")]
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