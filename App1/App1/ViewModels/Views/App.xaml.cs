using App1.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public partial class App : Application
    {
        public static double ScreenWidth { get; internal set; }
        public static double ScreenHeight { get; internal set; }
        public static bool IsUserLoggedIn { get; internal set; }
        //SQLiteManager database;

        public App()
        {
            InitializeComponent();

             // database = new SQLiteManager();

                MainPage = new NavigationPage(new ChoosePage())
                {
                    BarTextColor = Color.FromHex("#FFFFFF"),
                    BarBackgroundColor = Color.FromHex("#F06560"), 
                    

                };

         //   MainPage = new PetitTest(); 

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
