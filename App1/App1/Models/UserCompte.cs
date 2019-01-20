using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public  class UserCompte
    {   
        [PrimaryKey, AutoIncrement]
        public int UserCompteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public string ImageURL { get; set; }
        public string GuideEmail { get; set; }

        public int GuideCompteId { get; set; }

        public GuideCompte GuideCompte { get; set; }

        public UserCompte()
        {
            GuideCompte = new GuideCompte();
        }
    }
}
