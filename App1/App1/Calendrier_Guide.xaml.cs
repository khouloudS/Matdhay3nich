using RestClient.Client;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using App1.Models;

namespace App1
{
    public partial class Calendrier_Guide : TabbedPage
    {
        GenericRestClient<Calendrier> Rest = new GenericRestClient<Calendrier>();
        GuideCompte GuideCompte = new GuideCompte();
        public Calendrier_Guide(GuideCompte G)
        {
            GuideCompte = G;
            InitializeComponent();
            Mainlist.ItemsSource = G.Calendriers;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void Enregister_Button(object sender, EventArgs e)
        {
            Calendrier guide = new Calendrier();

            guide.Title = Title_Entry.Text;
             guide.Date = DateTime.Now.ToString();
            guide.Editor = Note_Editor.Text;
            guide.GuideCompteId = GuideCompte.GuideCompteId;
            guide.GuideCompte = GuideCompte;
            await Rest.PostAsync(guide);

        }

    }
}