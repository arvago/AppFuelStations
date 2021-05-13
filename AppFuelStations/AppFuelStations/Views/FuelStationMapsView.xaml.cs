using AppFuelStations.Models;
using AppFuelStations.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppFuelStations.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FuelStationMapsView : ContentPage
    {
        public FuelStationMapsView(FuelStationModel fuelStationSelected)
        {
            InitializeComponent();

            //OBTIENE LA IMAGEN DE LA GASOLINERA SELECCIONADA PARA CONVERTIRLA DE BASE 64
            fuelStationSelected.Picture = new ImageService().SaveImageFromBase64(fuelStationSelected.Picture, fuelStationSelected.ID);
            MapFuelStations.FuelStation = fuelStationSelected;

            //CENTRA EL MAPA EN LA UBICACION GUARDADA DE LA GASOLINERA
            MapFuelStations.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(
                        fuelStationSelected.Latitude,
                        fuelStationSelected.Longitude
                        ),
                        Distance.FromMiles(.5)
                    )
                );

            //AGREGA EL PIN EN LA UBICACION GUARDADA DE LA GASOLINERA
            MapFuelStations.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = fuelStationSelected.Name,
                    Position = new Position(
                        fuelStationSelected.Latitude,
                        fuelStationSelected.Longitude
                        )
                }
            );
        }
    }
}