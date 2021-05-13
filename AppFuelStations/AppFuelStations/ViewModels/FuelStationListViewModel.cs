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
        //CREAMOS NUESTRA INSTANCIA
        private static FuelStationListViewModel instance;

        //CREAMOS NUESTRO COMANDO QUE SE UTILIZARA HACIENDO REFERENCIA A SU METODO
        Command _NewFuelStationCommand;
        public Command NewFuelStationCommand => _NewFuelStationCommand ?? (_NewFuelStationCommand = new Command(NewFuelStationAction));
        
        //GET Y SET DE LA LISTA QUE GUARDARA LAS GASOLINERAS
        List<FuelStationModel> fuelStations;
        public List<FuelStationModel> FuelStations
        {
            get => fuelStations;
            set => SetProperty(ref fuelStations, value);
        }

        //GET Y SET DE LA GASOLINERA SELECCIONADA
        FuelStationModel fuelStationSelected;
        public FuelStationModel FuelStationSelected
        {
            get => fuelStationSelected;
            set
            {
                if (
                SetProperty(ref fuelStationSelected, value))
                {
                    SelectAction();
                }
            }
        }

        //CARGA LA LISTA DESDE EL PRINCIPIO DE LA APLICACION
        public FuelStationListViewModel()
        {
            instance = this;
            LoadFuelStations();
        }
        //METODO UNICO PARA RETORNAR NUESTRA INSTANCIA
        public static FuelStationListViewModel GetInstance()
        {
            return instance;
        }

        //METODO PARA OBTENER TODAS LAS GASOLINERAS DEL SQLITE
        public async void LoadFuelStations()
        {
            //GUARDA TODAS LAS GASOLINERAS EN FUELSTATIONS
            FuelStations = await App.SQLiteDatabase.GetAllFuelStationAsync();
        }

        //METODO PARA INVOCAR AL DETAILVIEW PARA AGREGAR UNA GASOLINERA
        private void NewFuelStationAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FuelStationDetailPage());

        }

        //METODO PARA INVOCAR EL DETAILVIEW PARA ACTUALIZAR LA INFORMACION DE LA GASOLINERA SELECCIONADA
        private void SelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FuelStationDetailPage(fuelStationSelected));

        }
        
    }
}
