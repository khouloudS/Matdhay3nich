using App1.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using RestClient.Client;
using App1.Client;
using Plugin.Geolocator;


namespace App1
{
    public partial class GuideSignInPage : ContentPage
    {
        GenericRestClient<GuideCompte> restClient = new GenericRestClient<GuideCompte>();
        //GenericRestClient<UserCompte> restClient1 = new GenericRestClient<UserCompte>();
    //    public Plugin.Geolocator.Abstractions.Position position { get; set; }
        public GuideSignInPage()
        {
            InitializeComponent();

        }

        async void ButtonCreatAccount_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Guide_Account());
        }

        public void ButtonSinIn_OnClicked(object sender, EventArgs e)
        {
            var guide = new GuideCompte
            {
                Email = email.Text,
                Password = password.Text
            };

            var isValid = AreCredentialsCorrect(guide);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                LoginGuide();
            }
            else
            {
                messageLabel.Text = "Please field are empty";
            }
        }

        bool AreCredentialsCorrect(GuideCompte guide)
        {
            return guide.Email != "" && guide.Password != "";
        }


        
         public async void LoginGuide()
        {
           
            var Email = email.Text;
            var Password = password.Text;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
          
            GuideCompte User = await restClient.GetGuideCompteByEmail(Email);

            if (User.Password.Equals(Password))
            {
                Navigation.InsertPageBefore(new Map_Guide(User), this);
                await Navigation.PopAsync();
            }
        }
           
            }

 }


    