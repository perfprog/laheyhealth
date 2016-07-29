using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    public class Participant
    {
        private int id;
        private Language language;
        private DateTime startDt;
        private DateTime completedDt;
        private String ipAddress;

        public String IPAdress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }


        public DateTime CompleteDt
        {
            get { return completedDt; }
            set { completedDt = value; }
        }

        public DateTime StartDt
        {
            get { return startDt; }
            set { startDt = value; }
        }

        public Language Language
        {
            get { return language; }
            set { language = value; }
        }



        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //Method used to send e-mail - doesn't store e-mail not to deal with privacy issues
        //Returns boolean that informs if e-mail was sent correctly
        public bool sendScores(String eMail) {
            throw new NotImplementedException("Method not yet implemented");
        }

    }
}