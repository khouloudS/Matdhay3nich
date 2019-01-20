using App1.Models;
using Plugin.Geolocator;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class PositionPage : ContentPage
    {

        GuideCompte GuideCompte = new GuideCompte();
        public PositionPage(GuideCompte G)
        {
            InitializeComponent();
            GuideCompte = G;
            getuser();
        }


      //private async void b1(object sender, EventArgs e)
      //  {
      //      var locator = CrossGeolocator.Current;
      //      locator.DesiredAccuracy = 50;
      //      var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
      //      LatitudeLabel.Text = position.Latitude.ToString();
      //      LongitudeLabel.Text  = position.Longitude.ToString();
      //  }

        public void getuser()
        {
            List <UserCompte> l = new List<UserCompte>();

            l = GuideCompte.UserComptes.ToList();
            Mainlist.ItemsSource = l;
        }
       
    }
}
