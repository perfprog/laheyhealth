using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaheyHealth.Models
{
    //No need for error messages since Participants are not inserted from front end
    public class Participant
    {
        private int id;
        private Language language;
        private DateTime startDt;
        private DateTime completedDt;
        private String ipAddress;
        [Required]
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
        [Required]
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