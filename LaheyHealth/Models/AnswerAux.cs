using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class AnswerAux
    {
        public int Id{ get; set; }
        public int IdSelectedSkill { get; set; }
        public int IdSelectedImportance { get; set; }
        //This is set to true when the user sets values for value fields (Skill, Importance)
        public bool AllAssigned { get; set; }
    }
}