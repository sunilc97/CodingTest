using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using XamarinForms.SQLite.SQLite;

namespace WhetherPOC
{
    public partial class FavoritiesPage : ContentPage
    {
        private readonly SQLiteConnection _sqLiteConnection;

        public FavoritiesPage()
        {
            InitializeComponent();
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();


            var listView = new ListView
            {
               // ItemsSource=_sqLiteConnection.Table<>();
            };
           // lstView.Children.Add(listView);

        }
    }
}
