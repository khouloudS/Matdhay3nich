using App1.Models;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class Delete_User : ContentPage
    {
        public GenericRestClient<UserCompte> rest = new GenericRestClient<UserCompte>();
        GuideCompte GuideCompte = new GuideCompte();
        //private readonly List<string> _names = new List<string>
        //    {
        //        "Ali","Ahmed","Salah","Rafik","Khouloud","Syrine","Mohamed","Hela","Ons"
        //    };
        public Delete_User(GuideCompte G)
        {
            GuideCompte = G;
            InitializeComponent();
            //MainListView.ItemsSource = _names;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MainListView.ItemsSource = await rest.GetAsync();
        }

        private async void Search_Delete_User_Button(object sender, EventArgs e)
        {
            string KeyWord = Search_Delete_User.Text;
         
            MainListView.ItemsSource =await rest.GetUserCompteAsync(KeyWord);
        }

   
        private void Delete_Clicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            if (menuitem != null)
            {
                var name = menuitem.BindingContext as string;
                DisplayAlert("Alert", "Delete" + name, "ok");
            }
        }
       

    }
}
