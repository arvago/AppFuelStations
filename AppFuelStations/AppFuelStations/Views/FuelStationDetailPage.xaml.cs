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
        public FuelStationDetailPage()
        {
            InitializeComponent();
            BindingContext = new FuelStationDetailViewModel();
        }

        public FuelStationDetailPage(FuelStationModel fuelStationSelected)
        {
            InitializeComponent();
            BindingContext = new FuelStationDetailViewModel(fuelStationSelected);
        }

        
    }
}