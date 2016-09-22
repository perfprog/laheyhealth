using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Scores
    {
        private int id;
        //Score is not required for now, this might change since we will 
        //robably have the calculation made before it is inserted to the database.
        private int score;
        private Participant participant;
        private Language language;
        private Scale scale;
        private Subscale subscale;
        //Values for questions: We can add in new ones in the future if necesary
        //To calculate result we need to get value for skill and importance and multiply them with each other
        private ImportanceValues importanceValues;
        private SkillValues skillValues;
        //Question it is related to
        private Item item;

        [Required]

        public virtual Item Item
        {
            get { return item; }
            set { item = value; }
        }
        
        public virtual SkillValues SkillValues
        {
            get { return skillValues; }
            set { skillValues = value; }
        }
        

        public virtual ImportanceValues ImportanceValues
        {
            get { return importanceValues; }
            set { importanceValues = value; }
        }
        
        public virtual Subscale Subscale
        {
            get { return subscale; }
            set { subscale = value; }
        }
        

        public virtual Scale Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        

        public virtual Language Language
        {
            get { return language; }
            set { language = value; }
        }
        public virtual Participant Participant
        {
            get { return participant; }
            set { participant = value; }
        }

        public virtual int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}