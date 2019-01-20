using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class ChoosePage : ContentPage
    {
        public ChoosePage()
        {
            InitializeComponent();
        }
        public async void OnGuideSingUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GuideSignInPage());
        }

        public async void OnUserSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSignInPage());
        }
    }
}
