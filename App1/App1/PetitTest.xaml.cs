using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class PetitTest : ContentPage
    {

        public ObservableCollection<string> MyList { get; set; } = new ObservableCollection<string>();
        public DateTime StartTime { get; set; }
        public PetitTest()
        {
            InitializeComponent();
            StartTime = DateTime.Now;
            BindingContext = this;
            MyList.Add((DateTime.Now.Subtract(StartTime)).ToString());

            var loc = CrossGeolocator.Current;

            loc.StartListeningAsync(1, 0.001, true);
            loc.PositionChanged += Loc_PositionChanged;
        }

        private void Loc_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MyList.Add(DateTime.Now.Subtract(StartTime).ToString());
            DisplayAlert("Test", "Position has changed", "Ok");
          
        }
    }
}
