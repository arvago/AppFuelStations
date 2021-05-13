using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppFuelStations.Droid.Renders;
using AppFuelStations.Models;
using AppFuelStations.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyMap), typeof(MyMapRenderer))]
namespace AppFuelStations.Droid.Renders
{
    public class MyMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        FuelStationModel FuelStation;

        public MyMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.FuelStation = (e.NewElement as MyMap).FuelStation;
            }

        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            //return base.CreateMarker(pin);
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(FuelStation.Latitude, FuelStation.Longitude));
            marker.SetTitle(FuelStation.Name);
            marker.SetSnippet($"{FuelStation.Brand}");
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MapWindow, null);
                var infoImage = view.FindViewById<ImageView>(Resource.Id.MapWindowImage);
                var infoName = view.FindViewById<TextView>(Resource.Id.MapWindowName);
                var infoBreedAge = view.FindViewById<TextView>(Resource.Id.MapWindowBrand);

                if (infoImage != null) infoImage.SetImageBitmap(BitmapFactory.DecodeFile(FuelStation.Picture));
                if (infoName != null) infoName.Text = FuelStation.Name;
                if (infoBreedAge != null) infoBreedAge.Text = $"{FuelStation.Brand}";

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
                
    }
}