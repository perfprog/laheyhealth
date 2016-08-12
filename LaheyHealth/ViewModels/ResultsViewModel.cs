using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaheyHealth.Models;
namespace LaheyHealth.ViewModels
{
    public class ResultsViewModel
    {
        private List<Scores> lstScores;

        public List<Scores> LstScores
        {
            get { return lstScores; }
            set { lstScores = value; }
        }

        public bool lstEmpty()
        {
            return lstScores.Count > 0;
        }


    }
}