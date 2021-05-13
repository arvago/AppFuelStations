using AppFuelStations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace AppFuelStations.Renders
{
    //SE CREA LA CLASE MyMap, que se utilizara para el futuro render en el mapa
    public class MyMap : Map
    {
        public FuelStationModel FuelStation; //Iniciamos nuestra variable publica del tipo FuelStationModel
    }
}
