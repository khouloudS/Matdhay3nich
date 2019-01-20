using App1.Models;
using Plugin.Media;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class SettingGuideProfile : ContentPage
    {
        GenericRestClient<GuideCompte> rest = new GenericRestClient<GuideCompte>();
        GuideCompte GuideCompte = new GuideCompte();
        public SettingGuideProfile(GuideCompte G)
        {
            GuideCompte = G;
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FirstName.Text = GuideCompte.FirstName;
            LastName.Text = GuideCompte.LastName;
            Phone.Text = GuideCompte.Phone;
            Phone.Text = GuideCompte.Phone;
            Email.Text = GuideCompte.Email;
            Password.Text = GuideCompte.Password;
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
        private async void buttonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings_Guide(GuideCompte));
        }
    }
}