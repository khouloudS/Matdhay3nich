using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class Calendrier_User : ContentPage
    {
        UserCompte UserCompte = new UserCompte();
        public Calendrier_User(UserCompte U)
        {
            UserCompte = U;
            InitializeComponent();
            Mainlist.ItemsSource = U.GuideCompte.Calendriers;
        }
    }
}
