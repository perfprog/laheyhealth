using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Scale
    {
        private int id;
        private String name;
        private Language language;

        public Language Language
        {
            get { return language; }
            set { language = value; }
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