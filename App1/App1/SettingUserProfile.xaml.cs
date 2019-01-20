using App1.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class SettingUserProfile : ContentPage
    {
        UserCompte UserCompte = new UserCompte();
        public SettingUserProfile(UserCompte U)
        {
            UserCompte = U;
            InitializeComponent();
        }

     

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FirstName.Text = UserCompte.FirstName;
            LastName.Text = UserCompte.LastName;
            Phone.Text = UserCompte.Phone;
            Phone.Text = UserCompte.Phone;
            Email.Text = UserCompte.Email;
            Password.Text = UserCompte.Password;
        }

        private async void ImpoterPhoto_OnClicked(object sender, EventArgs e)
        {
            await Plugin.Media.CrossMedia.Current.Initialize();
            if (!Plugin.Media.CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Oops", "Pick photo is not avaible", "Ok");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
            if (file == null)
            {
                return;

            }

            MainImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        private async void PrendrePhoto_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "😞 No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(options: new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            MainImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        private async void buttonCancel_Clicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings_User(UserCompte));
        }
    }
}
