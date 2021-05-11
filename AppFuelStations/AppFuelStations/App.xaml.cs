using AppFuelStations.Data;
using AppFuelStations.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuelStations
{
    public partial class App : Application
    {
        private static SQLiteDatabase _SQLiteDatabase;
        public static SQLiteDatabase SQLiteDatabase
        {
            get
            {
                if (_SQLiteDatabase == null) _SQLiteDatabase = new SQLiteDatabase();
                return _SQLiteDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new FuelStationListPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
