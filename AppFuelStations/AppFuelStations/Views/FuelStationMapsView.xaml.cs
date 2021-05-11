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

            fuelStationSelected.Picture = new ImageService().SaveImageFromBase64(fuelStationSelected.Picture, fuelStationSelected.ID);
            MapFuelStations.FuelStation = fuelStationSelected;

            MapFuelStations.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(
                        fuelStationSelected.Latitude,
                        fuelStationSelected.Longitude
                        ),
                        Distance.FromMiles(.5)
                    )
                );

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