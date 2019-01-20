using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public class GuideCompte
    {
        public GuideCompte()
        {
            this.UserComptes = new HashSet<UserCompte>();
            this.Calendriers = new HashSet<Calendrier>();
        }
        [PrimaryKey, AutoIncrement]
        public int GuideCompteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public string ImageURL { get; set; }

        public ICollection<UserCompte> UserComptes { get; set; }
        public ICollection<Calendrier> Calendriers { get; set; }
    }
}