using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class SkillValues
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
        [Required(ErrorMessage = "Value is required")]

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }
        [Required(ErrorMessage = "Label is required")]
        public String Label
        {
            get { return label; }
            set { label = value; }
        }
        [Required(ErrorMessage = "Field is required (should be autofilled as Skill)")]
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