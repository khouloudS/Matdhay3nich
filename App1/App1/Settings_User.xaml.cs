using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class Settings_User : ContentPage
    {
        UserCompte UserCompte = new UserCompte();
        public Settings_User(UserCompte U)
        {
            UserCompte = U;
            InitializeComponent();
        }
       
        async void Setting_User_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingUserProfile(UserCompte));
        }
        async void Sign_out_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChoosePage());
        }
    }
}
