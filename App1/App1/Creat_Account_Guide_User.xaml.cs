using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class Creat_Account_Guide_User : ContentPage
    {
        public Creat_Account_Guide_User()
        {
            InitializeComponent();
        }

        async void ButtonGuide_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Guide_Account());
        }
        async void ButtonUser_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new User_Account());
        }

      async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChoosePage());
        }
    }
}
