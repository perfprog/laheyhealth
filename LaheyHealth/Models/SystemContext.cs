using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class SystemContext:DbContext
    {
        private DbSet<Participant> participant;
        private DbSet<Language> language;
        private DbSet<Item> item;
        private DbSet<Scale> scale;
        private DbSet<Scores> scores;
        private DbSet<Subscale> subscale;
        private DbSet<ImportanceValues> importanceValues;
        private DbSet<SkillValues> skillValues;

        public DbSet<SkillValues> SkillValues
        {
            get { return skillValues; }
            set { skillValues = value; }
        }

        public DbSet<ImportanceValues> ImportanceValues
        {
            get { return importanceValues; }
            set { importanceValues = value; }
        }

        public DbSet<Subscale> Subscale
        {
            get { return subscale; }
            set { subscale = value; }
        }


        public DbSet<Scores> Scores
        {
            get { return scores; }
            set { scores = value; }
        }

        public DbSet<Scale> Scale
        {
            get { return scale; }
            set { scale = value; }
        }


        public DbSet<Item> Item
        {
            get { return item; }
            set { item = value; }
        }

        public DbSet<Language> Language
        {
            get { return language; }
            set { language = value; }
        }

        public DbSet<Participant> Participant
        {
            get { return participant; }
            set { participant = value; }
        }

        public SystemContext() : base("DefaultConnection") { }
        //public SystemContext() : base("localConnection") { }


    }
}