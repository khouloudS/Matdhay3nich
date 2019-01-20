using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
         public   class Calendrier
    {
        public int CalendrierId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Editor { get; set; }

        public int GuideCompteId { get; set; }
       
        public GuideCompte GuideCompte { get; set; }
    }
}
