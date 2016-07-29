using System;
using System.Collections.Generic;
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

        public Subscale Subscale
        {
            get { return subscale; }
            set { subscale = value; }
        }

        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        public Scale Scale
        {
            get { return scale; }
            set { scale = value; }
        }

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