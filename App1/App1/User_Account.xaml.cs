using App1.Client;
using App1.Models;
using Plugin.Geolocator;
using Plugin.Media;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class User_Account : ContentPage
    {
        GenericRestClient<UserCompte>  Rest = new GenericRestClient<UserCompte>();
        public User_Account()
        {
            InitializeComponent();
        }

        async void ButtonCancel_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSignInPage());
        }

        private async void ButtonSinIn_OnClicked(object sender, EventArgs e)
          {

            if ((string.IsNullOrEmpty(Phone.Text)) || (Phone.Text.Length <= 8))
            {
                await DisplayAlert("Erreur", " :O  ", "ok");
            }
            else
            {

          

            UserCompte E = new UserCompte();
         

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            

            E.FirstName = FirstName.Text;
            E.LastName = LastName.Text;
            E.Phone = Phone.Text;
            E.Email = EmailUser.Text;
            E.Password = Password.Text;
          //  E.GuideEmail = GuideEmail.Text;
            E.Lattitude = position.Latitude;
            E.Longitude = position.Longitude;
           

            await Rest.PostAsync(E);
        }
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
    }
}
