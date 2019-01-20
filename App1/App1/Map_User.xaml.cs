using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Messaging;
using App1.Client;
using App1.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace App1
{
    public partial class Map_User : ContentPage
    {
        public UserCompte UserCompte { get; set; }
        public GuideCompte GuideCompte { get; set; }
        public Map_User(UserCompte U)
        {
            
            InitializeComponent();
            UserCompte = U;
            setMap();
            var x = distance();
            if (x>=0)
            {
                DisplayAlert("ok", "Attention vous dépassez votre distance limitée", "Cancel");
            }

        }

        public Map_User(UserCompte user , GuideCompte guide)
        {
            InitializeComponent();
            UserCompte = user;
            GuideCompte = guide;
            setMap();
        }

        private double distance()
        {
            var lat1 = UserCompte.Lattitude;
            var lon1 = UserCompte.Longitude;

            var lat2 = UserCompte.GuideCompte.Lattitude;
            var lon2 = UserCompte.GuideCompte.Longitude;

         //   var lat2 = GuideCompte.Lattitude;
           // var lon2 = GuideCompte.Longitude;

            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            //if (unit == 'K')
            //{
            //    dist = dist * 1.609344;
            //}
            //else if (unit == 'N')
            //{
            //    dist = dist * 0.8684;
            //}
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        
        private async void setMap()
        {
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserCompte.Lattitude, UserCompte.Longitude), Distance.FromKilometers(5)));



            var pin = new Pin
            {
                Position = new Position(UserCompte.Lattitude, UserCompte.Longitude),
                Label = "your position",
                Address = "adresse #1"
            };

            var pin2 = new Pin
            {
                Position = new Position(GuideCompte.Lattitude, GuideCompte.Longitude),
                Label = "Your guide position",
                Address = "adresse #1",

            };
            pin.Clicked += Pin_Clicked;
            pin2.Clicked += Pin_Clicked;

            MyMap.Pins.Add(pin);
            MyMap.Pins.Add(pin2);

            for (int i = 0; i < 20; i++)
            {
                await LocateUserPosition(UserCompte, pin);

                await Task.Delay(30000);
            }
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
            
        //    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(UserCompte.Lattitude, UserCompte.Longitude), Distance.FromKilometers(5)));



        //    var pin = new Pin
        //    {
        //        Position = new Position(UserCompte.Lattitude, UserCompte.Longitude),
        //        Label = "your position",
        //        Address = "adresse #1"
        //    };

        //    var pin2 = new Pin
        //    {
        //        Position = new Position(UserCompte.GuideCompte.Lattitude, UserCompte.GuideCompte.Longitude),
        //        Label = "Your guide position",
        //        Address = "adresse #1",
                
        //    };
        //    pin.Clicked += Pin_Clicked;
        //    pin2.Clicked += Pin_Clicked;

        //    MyMap.Pins.Add(pin);
        //    MyMap.Pins.Add(pin2);

        //    for (int i = 0; i < 20; i++)
        //    {
        //        await LocateUserPosition(UserCompte, pin);

        //        await Task.Delay(30000);
        //    }
        //}

        private async Task LocateUserPosition(UserCompte userCompte, Pin pin)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            pin.Position = new Position(position.Latitude, position.Longitude);
            //TODO add new pos
            userCompte.Lattitude = position.Latitude;
            userCompte.Longitude = position.Longitude;
            await UpdateUserPositionOnWebServiveAsync(userCompte.UserCompteId, userCompte);
        }

        public async Task<bool> UpdateUserPositionOnWebServiveAsync(int id, UserCompte compte)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(compte);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync("http://matdhaya3nich2.azurewebsites.net/api/UserComptes/" + id, httpContent);

            return result.IsSuccessStatusCode;
        }

        private void Pin_Clicked(object sender, EventArgs eventArgs)
        {


            var selectedPin = sender as Pin;
         
            DisplayAlert(selectedPin?.Label, selectedPin?.Address, "OK");



        }

        async void Settings_User(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings_User(UserCompte));
        }

        async void Calendar_User(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Calendrier_User(UserCompte));
        }

       private void Call_Guide(object sender, EventArgs e)
        {
            var num = GuideCompte.Phone;
            var nom = GuideCompte.LastName;
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+216"+num, nom);
            }
        }
    }
}
