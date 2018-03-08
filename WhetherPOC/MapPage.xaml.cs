using System;
using System.Collections.Generic;
using SQLite;
using WhetherPOC.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinForms.SQLite.SQLite;

namespace WhetherPOC
{
    public partial class MapPage : ContentPage
    {
        private readonly SQLiteConnection _sqLiteConnection;
        Wheather data= new Wheather();
        ListView listView;
        public MapPage(Wheather whether)
        {
            InitializeComponent();

            // Name.Text = whether.wind.deg.ToString();
            Name.Text = whether.main.temp.ToString();

            Country.Text = whether.sys.country;

       
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();

            _sqLiteConnection.CreateTable<WhetherDataModel>();
            listView = new ListView
            {
                ItemsSource = _sqLiteConnection.Table<WhetherDataModel>()
            };
           
            dataName.Children.Add(listView);
            data = whether;

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                googlemap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.39, -122.09), Distance.FromMeters(500)));

            });

           
        }

        void addFavorite(object sender, System.EventArgs e)
        {
            _sqLiteConnection.Insert(new WhetherDataModel
            {
                name = data.name,
                country = data.sys.country,
            });

            listView.ItemsSource = _sqLiteConnection.Table<WhetherDataModel>();
           
           
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
                
            var pin = new Pin()
            {
                Type = PinType.Place,
                Position = new Position(Convert.ToDouble(data.coord.lat), Convert.ToDouble(data.coord.lon)),
                Label = "test",
                Address = "test"

            };
            googlemap.Pins.Add(pin);
           
        }
    }
}
