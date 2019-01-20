using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class Settings_Guide : ContentPage
    {
        GuideCompte GuideCompte = new GuideCompte();
        public Settings_Guide(GuideCompte G)
        {
            GuideCompte = G;
            InitializeComponent();

        }
         async void Delete_User_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Delete_User(GuideCompte));
        }
        async void Setting_Guide_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingGuideProfile(GuideCompte));
        }
       async void Sign_out_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChoosePage());
        }
        


    }
}
