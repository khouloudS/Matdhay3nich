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
    public partial class chooseguide : ContentPage
    {
        public chooseguide(UserCompte user)
        {
            InitializeComponent();

            Init();

            MainListView.ItemTapped += async (sender, e) =>
            {
                var item = e.Item as GuideCompte;
                var client = new GenericRestClient<UserCompte>();
                var x = user;
                x.GuideCompteId = item.GuideCompteId;
                x.GuideCompte = null;
                var isDone = await client.PutAsync(user.UserCompteId, x);
                await Navigation.PushAsync(new Map_User(x, item));
            };
        }
        
        private async void Init()
        {
            var list = await new GenericRestClient<GuideCompte>().GetAsync();
            MainListView.ItemsSource = list;
        }
    }
}
