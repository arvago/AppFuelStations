using AppFuelStations.Models;
using AppFuelStations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFuelStations.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FuelStationDetailPage : ContentPage
    {
        //CONSTRUCTOR QUE SE INVOCA PARA AGREGAR UNA GASOLNERA
        public FuelStationDetailPage()
        {
            InitializeComponent();
            //ENVIA Y OBTIENE LOS BINDINGS DEL VIEW MODEL
            BindingContext = new FuelStationDetailViewModel();
        }

        //CONSTRUCTOR QUE SE INVOCA PARA ACTUALIZAR UNA GASOLINERA
        public FuelStationDetailPage(FuelStationModel fuelStationSelected)
        {
            InitializeComponent();
            //ENVIA Y OBTIENE LOS BINDINGS DEL VIEW MODEL
            BindingContext = new FuelStationDetailViewModel(fuelStationSelected);
        }

        
    }
}