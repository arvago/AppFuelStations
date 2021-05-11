using System;
using AppFuelStations.Models;
using AppFuelStations.Views;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppFuelStations.ViewModels
{
    public class FuelStationListViewModel : BaseViewModel
    {
        private static FuelStationListViewModel instance;

        Command _NewFuelStationCommand;
        public Command NewFuelStationCommand => _NewFuelStationCommand ?? (_NewFuelStationCommand = new Command(NewFuelStationAction));

        List<FuelStationModel> fuelStations;
        public List<FuelStationModel> FuelStations
        {
            get => fuelStations;
            set => SetProperty(ref fuelStations, value);
        }

        FuelStationModel fuelStationSelected;
        public FuelStationModel FuelStationSelected
        {
            get => fuelStationSelected;
            set
            {
                if (SetProperty(ref fuelStationSelected, value))
                {
                    SelectAction();
                }
            }
        }

        public FuelStationListViewModel()
        {
            instance = this;
            LoadFuelStations();
        }

        public static FuelStationListViewModel GetInstance()
        {
            return instance;
        }

        public async void LoadFuelStations()
        {
            FuelStations = await App.SQLiteDatabase.GetAllFuelStationAsync();
        }

        private void NewFuelStationAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FuelStationDetailPage());

        }

        private void SelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FuelStationDetailPage(fuelStationSelected));

        }
        
    }
}
