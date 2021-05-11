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
    public partial class FuelStationListPage : ContentPage
    {
        public FuelStationListPage()
        {
            InitializeComponent();

            BindingContext = new FuelStationListViewModel();
        }
    }
}