using AppFuelStations.Models;
using AppFuelStations.Services;
using AppFuelStations.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppFuelStations.ViewModels
{
    public class FuelStationDetailViewModel : BaseViewModel
    {
        //INICIALIZAMOS NUESTROS COMANDOS QUE SE UTILIZARAN AL MANEJAR EL DETAIL VIEW
        Command saveCommand;
        public Command SaveCommand => saveCommand ?? (saveCommand = new Command(SaveAction));
        Command deleteCommand;
        public Command DeleteCommand => deleteCommand ?? (deleteCommand = new Command(DeleteAction));
        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));
        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));
        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        //SE CREAN LOS CONSTRUCTORES PARA PODER SETEAR LOS VALORES
        FuelStationModel fuelStationSelected;
        public FuelStationModel FuelStationSelected
        {
            get => fuelStationSelected;
            set => SetProperty(ref fuelStationSelected, value);
        }

        string imageBase64;
        public string ImageBase64
        {
            get => imageBase64;
            set => SetProperty(ref imageBase64, value);
        }

        double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        double _GreenPrice;
        public double GreenPrice
        {
            get => _GreenPrice;
            set => SetProperty(ref _GreenPrice, value);
        }

        double _RedPrice;
        public double RedPrice
        {
            get => _RedPrice;
            set => SetProperty(ref _RedPrice, value);
        }
        double _DieselPrice;
        public double DieselPrice
        {
            get => _DieselPrice;
            set => SetProperty(ref _DieselPrice, value);
        }

        //CONSTRUCTOR QUE SE INVOCA AL QUERER CREAR UNA NUEVA GASOLINERA
        public FuelStationDetailViewModel()
        {
            FuelStationSelected = new FuelStationModel();
        }

        //CONSTRUCTOR QUE SE INVOCA AL QUERER EDITAR/ACTUALIZAR LA INFO DE UNA GASOLINERA
        public FuelStationDetailViewModel(FuelStationModel fuelStationSelected)
        {
           FuelStationSelected = fuelStationSelected;
            ImageBase64 = fuelStationSelected.Picture;
            Latitude = fuelStationSelected.Latitude;
            Longitude = fuelStationSelected.Longitude;
            GreenPrice = fuelStationSelected.GreenPrice;
            RedPrice = fuelStationSelected.RedPrice;
            DieselPrice = fuelStationSelected.DieselPrice;
        }
        //METODO PARA GUARDAR LOS DATOS EN SQLITE
        private async void SaveAction()
        {
            fuelStationSelected.Latitude = Latitude;
            fuelStationSelected.Longitude = Longitude;
            fuelStationSelected.GreenPrice = GreenPrice;
            fuelStationSelected.RedPrice = RedPrice;
            fuelStationSelected.DieselPrice = DieselPrice;
            await App.SQLiteDatabase.SaveFuelStationAsync(fuelStationSelected);
            FuelStationListViewModel.GetInstance().LoadFuelStations();//LINEA PARA RECARGAR LOS DATOS GUARDADOS 
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        //METODO PARA ELIMINAR LOS DATOS DE SQLITE
        private async void DeleteAction()
        {
            await App.SQLiteDatabase.DeleteFuelStationAsync(fuelStationSelected);
            FuelStationListViewModel.GetInstance().LoadFuelStations();//LINEA PARA RECARGAR LOS DATOS GUARDADOS 
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        //METODO PARA MANDAR LOS VALORES DE LA GASOLINERA SELECCIONADA, PARA SU MUESTRA EN EL MAPA
        private void MapAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(
                  new FuelStationMapsView(new FuelStationModel
                  {
                      ID = fuelStationSelected.ID,
                      Name = fuelStationSelected.Name,
                      Brand = fuelStationSelected.Brand,
                      Picture = fuelStationSelected.Picture,
                      Latitude = fuelStationSelected.Latitude,
                      Longitude = fuelStationSelected.Longitude
                  })
              );
        }

        //METODO PARA SELECCIONAR UNA FOTO DEL DISPOSITIVO
        private async void SelectPictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                //VALIDA LOS PERMISOS PARA SELECCIONAR FOTO
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("AppFuelStations", "No es posible seleccionar fotografías en el dispositivo", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null)
                    return;
                //ASIGNA LA FOTO DESPUES DE CONVERTIRLA A BASE 64
                FuelStationSelected.Picture = ImageBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppFuelStations", $"Se generó un error al tomar la fotografía ({ex.Message})", "Ok");
            }
        }

        //METODO PARA OBTENER LAS COORDENADAS DEL DISPOSITVO Y GUARDARLAS
        private async void GetLocationAction()
        {
            try
            {
                FuelStationSelected.Latitude = FuelStationSelected.Longitude = 0;
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    //GUARDA LAS COORDENADAS EN EL MODELO, Y LAS MUESTRA EN EL DETAIL, GRACIAS AL BINDING
                    Latitude = FuelStationSelected.Latitude = location.Latitude;
                    Longitude = FuelStationSelected.Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Application.Current.MainPage.DisplayAlert("AppFuelStations", $"El GPS no está soportado en el dispositivo ({fnsEx.Message})", "Ok");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Application.Current.MainPage.DisplayAlert("AppFuelStations", $"El GPS no está activado en el dispositivo ({fneEx.Message})", "Ok");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Application.Current.MainPage.DisplayAlert("AppFuelStations", $"No se pudo obtener el permiso para las coordenadas ({pEx.Message})", "Ok");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Application.Current.MainPage.DisplayAlert("AppFuelStations", $"Se generó un error al obtener las coordenadas del dispositivo ({ex.Message})", "Ok");
            }
        }
    }
}
