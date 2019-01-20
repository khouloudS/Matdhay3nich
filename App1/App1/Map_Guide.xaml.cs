using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms;

using XamarinForms.Models;
using App1.Models;
using Xamarin.Forms.Maps;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace App1
{
    public partial class Map_Guide : ContentPage
    {

        public GuideCompte GuideCompte { get; set; }
        public UserCompte UserCompte { get; set; }
        //Plugin.Geolocator.Abstractions.Position Position = new Plugin.Geolocator.Abstractions.Position();
        public Map_Guide(GuideCompte G)
        {
            
            InitializeComponent();
            GuideCompte = G;

        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(GuideCompte.Lattitude, GuideCompte.Longitude), Distance.FromKilometers(5)));



            var pin = new Pin
            {
                Position = new Position(GuideCompte.Lattitude, GuideCompte.Longitude),
                Label = "your position",
                Address = "adresse #1"
            };


            pin.Clicked += Pin_Clicked;
            // pin2.Clicked += Pin_Clicked;

            MyMap.Pins.Add(pin);
            //  MyMap.Pins.Add(pin2);
            foreach (var u in GuideCompte.UserComptes)
            {
                var pin1 = new Pin
                {
                    Position = new Position(u.Lattitude, u.Longitude),
                    Label = u.FirstName,
                    //  Address = u.adre
                };
                pin1.Clicked += Pin_Clicked;
                // pin2.Clicked += Pin_Clicked;

                MyMap.Pins.Add(pin1);
            }

            for (int i = 0; i < 20; i++)
            {
                await LocateUserPosition(GuideCompte, pin);

                await Task.Delay(30000);
            }
        }


        private async Task LocateUserPosition(GuideCompte guideCompte, Pin pin)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            pin.Position = new Position(position.Latitude, position.Longitude);
            GuideCompte.Longitude = position.Longitude;
            guideCompte.Lattitude = position.Latitude;
            await UpdateUserPositionOnWebServiveAsync(guideCompte.GuideCompteId, guideCompte);
        }

        public async Task<bool> UpdateUserPositionOnWebServiveAsync(int id, GuideCompte compte)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(compte);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync("http://matdhaya3nich2.azurewebsites.net/api/GuideComptes/" + id, httpContent);

            return result.IsSuccessStatusCode;
        }

        private void Pin_Clicked(object sender, EventArgs eventArgs)
        {


            var selectedPin = sender as Pin;

            DisplayAlert(selectedPin?.Label, selectedPin?.Address, "OK");



        }




        async void Settings_Guide(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings_Guide(GuideCompte));
        }

        async void Calendar_Guide(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Calendrier_Guide(GuideCompte));
        }

        async void user(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PositionPage(GuideCompte));
        }

      


    }
}


/*  protected override void OnAppearing()
               {
                   base.OnAppearing();
                   MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(36.89, 10.18), Distance.FromKilometers(5)));
                   var pin = new Pin
                   {
                       Position = new Position(36.891, 10.181),
                       Label = "position #1",
                       Address = "adresse #1"
                   };
                   var pin2 = new Pin
                   {
                       Position = new Position(36.892, 10.182),
                       Label = "position #2",
                       Address = "adresse #2"
                   };
                   var pin3 = new Pin
                   {
                       Position = new Position(36.893, 10.183),
                       Label = "position #3",
                       Address = "adresse #3"
                   };
           var guide = new GuideCompte();

           pin.Clicked += Pin_Clicked;
                   pin2.Clicked += Pin_Clicked;
                   pin3.Clicked += Pin_Clicked;


                   MyMap.Pins.Add(pin);
                   MyMap.Pins.Add(pin2);
                   MyMap.Pins.Add(pin3);




               }
               private void Pin_Clicked (object sender , EventArgs eventArgs)
               {
                   var selectedPin = sender as Pin;
                   DisplayAlert(selectedPin?.Label, selectedPin?.Address, "OK");
               }

   */
