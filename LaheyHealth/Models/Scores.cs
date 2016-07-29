using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Scores
    {
        private int id;
        private int score;
        private Participant participant;
        private Language language;
        private Scale scale;
        private Subscale subscale;
        //Values for questions: We can add in new ones in the future if necesary
        //To calculate result we need to get value for skill and importance and multiply them with each other
        private ImportanceValues importanceValues;
        private SkillValues skillValues;

        public SkillValues SkillValues
        {
            get { return skillValues; }
            set { skillValues = value; }
        }

        public ImportanceValues ImportanceValues
        {
            get { return importanceValues; }
            set { importanceValues = value; }
        }

        public Subscale Subscale
        {
            get { return subscale; }
            set { subscale = value; }
        }

        public Scale Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        public Participant Participant
        {
            get { return participant; }
            set { participant = value; }
        }

        public int Score
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