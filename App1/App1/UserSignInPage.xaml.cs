using App1.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

using RestClient.Client;
using App1.Client;

namespace App1
{
    public partial class UserSignInPage : ContentPage
    {
        GenericRestClient<UserCompte> restClient = new GenericRestClient<UserCompte>();
        public UserSignInPage()
        {
            InitializeComponent();
                
        }

        async void ButtonCreatAccount_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new User_Account());
        }

        public void ButtonSinIn_OnClicked(object sender, EventArgs e)
        {
            var userCompte = new UserCompte
            {
               Email = email.Text,
                Password = password.Text
            };

            var isValid = AreCredentialsCorrect(userCompte);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                LoginUser();
            }
            else
            {
                messageLabel.Text = "Please field are empty";
            }
        }
        

        bool AreCredentialsCorrect(UserCompte userCompte)
        {
            return userCompte.Email != "" && userCompte.Password != "";
        }

        public async void LoginUser()
        {
            var Email = email.Text;
            var Password = password.Text;
       

            UserCompte User = await restClient.GetUserCompteByEmail(Email);

            if(User.Password.Equals(Password))
            {
                await Navigation.PushAsync(new chooseguide(User));
                 //Navigation.InsertPageBefore(new Map_User(User), this);
                 //await Navigation.PopAsync();
            }

        }
    }
}
