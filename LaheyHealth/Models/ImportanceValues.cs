using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class ImportanceValues
    {
        private int id;
        private String type;
        private String label;
        private int value;
        private Language language;

        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        public int Value
        {
            get { return value; }
            set { value = value; }
        }

        public String Label
        {
            get { return label; }
            set { label = value; }
        }


        public String Type
        {
            get { return type; }
            set { type = value; }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}