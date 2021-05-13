using AppFuelStations.Models;
using AppFuelStations.Renders;
using AppFuelStations.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyMap), typeof(MyMapRenderer))]
namespace AppFuelStations.UWP.Renders
{
    public class MyMapRenderer : MapRenderer
    {
        MapControl NativeMap;
        FuelStationModel FuelStation;
        MapWindow FuelStationWindow;
        bool IsFuelStationWindowVisible = false; //SI EL CUADRO ESTA VISIBLE

        //LIMPIA LA INFORMACION QUE TENIA POR DEFECTO PARA CENTRAR EL MAPA Y EL PIN PERSONALIZADO
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.MapElementClick -= OnMapElementClick;
                NativeMap.Children.Clear();
                NativeMap = null;
                FuelStationWindow = null;
            }

            if (e.NewElement != null)
            {
                this.FuelStation = (e.NewElement as MyMap).FuelStation;

                var formsMap = (MyMap)e.NewElement;
                NativeMap = Control as MapControl;
                NativeMap.Children.Clear();
                NativeMap.MapElementClick += OnMapElementClick;

                //POSICION DEL PIN
                var position = new BasicGeoposition
                {
                    Latitude = FuelStation.Latitude,
                    Longitude = FuelStation.Longitude
                };
                var point = new Geopoint(position);

                //ATRIBUTOS DE NUESTRO MAPICON, SU FILEPATH, LOCACION, ANCHURA, ETC.
                var mapIcon = new MapIcon();
                mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///pin.png"));
                mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                mapIcon.Location = point;
                mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                NativeMap.MapElements.Add(mapIcon);
            }
        }

        //METODO PARA MOSTRAR EL RECUADRO CON LA INFO DE LA GASOLINERA EN EL MAPA, AL DAR CLICK AL PIN
        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapicon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            if (mapicon != null)
            {
                if (!IsFuelStationWindowVisible)
                { 
                    //MANDA LOS DATOS DE LA GASOLINERA AL MAPWINDOW PARA MOSTRAR EL RECUEADRO CON LA INFO
                    if (FuelStationWindow == null) FuelStationWindow = new MapWindow(FuelStation);
                    var position = new BasicGeoposition
                    {
                        Latitude = FuelStation.Latitude,
                        Longitude = FuelStation.Longitude
                    };
                    var point = new Geopoint(position);

                    NativeMap.Children.Add(FuelStationWindow);
                    MapControl.SetLocation(FuelStationWindow, point);
                    MapControl.SetNormalizedAnchorPoint(FuelStationWindow, new Windows.Foundation.Point(0.5, 1.0));

                    IsFuelStationWindowVisible = true;
                }
                else
                {
                    NativeMap.Children.Remove(FuelStationWindow);

                    IsFuelStationWindowVisible = false;
                }
            }
        }
    }
}
