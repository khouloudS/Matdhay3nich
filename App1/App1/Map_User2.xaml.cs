using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Plugin.Geolocator;
using App1.Models;
using RestClient.Client;

namespace App1
{
    public partial class Map_User2 : ContentPage
    {
        Pin guidePin;
        Pin userPin;
        Map map;
        public Map_User2(UserCompte user,GuideCompte guide)
        {
            InitializeComponent();

            Init(user,guide);
        }

        private async void Init(UserCompte user,GuideCompte guide)
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            map = new Map(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));

            guidePin = new Pin() { Position = new Position(guide.Lattitude, guide.Longitude), Label = guide.FirstName };
            userPin = new Pin() { Position = new Position(user.Lattitude, user.Longitude), Label = user.FirstName };

            map.Pins.Add(guidePin);
            map.Pins.Add(userPin);
            Content = map;


            await Refresh(user, guide);
        }

        private async Task Refresh(UserCompte user , GuideCompte guide)
        {
            for (int i = 0; i < 30; i++)
            {
                var uClient = new GenericRestClient<UserCompte>();
                var gClient = new GenericRestClient<GuideCompte>();

                var actualPosition = await CrossGeolocator.Current.GetPositionAsync();
                user.Lattitude = actualPosition.Latitude;
                user.Longitude = actualPosition.Longitude;
                await uClient.PostAsync(user);

                var guideActualPosition = await gClient.GetbyIdAsync(guide.GuideCompteId);
                guidePin.Position = new Position(guideActualPosition.Lattitude, guideActualPosition.Longitude);



                map.Pins.Clear();

                userPin.Position = new Position(user.Lattitude, user.Longitude);

                map.Pins.Add(guidePin);
                map.Pins.Add(userPin);

                await Task.Delay(30000);
                
            }
        }
    }
}
