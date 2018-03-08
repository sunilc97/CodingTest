using System;
using System.Diagnostics;
using Android.Locations;
using WhetherPOC.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(GetMyLocation))]
namespace WhetherPOC.Droid
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }

    }

    public class GetMyLocation: Java.Lang.Object,
                                ILocation,
                                    ILocationListener
    {
        LocationManager lm;
        public void OnProviderDisabled(string provider)
        {
            Xamarin.Forms.Application.Current.Properties["LocationEnable"] = false;
            Debug.WriteLine("Andriod Location disabled");

        }
        public void OnProviderEnabled(string provider)
        {
            Xamarin.Forms.Application.Current.Properties["LocationEnable"] = true;

            Debug.WriteLine("Andriod Location enabled");


        }
        public void OnStatusChanged(string provider,
            Availability status, Android.OS.Bundle extras)
        {

            Debug.WriteLine("Status Andriod" + status);


        }
        //---fired whenever there is a change in location---
        public void OnLocationChanged(Location location)
        {
            if (location != null)
            {
                LocationEventArgs args = new LocationEventArgs();
                args.lat = location.Latitude;
                args.lng = location.Longitude;
                locationObtained(this, args);
            };
        }




        public event EventHandler<ILocationEventArgs> locationObtained;


        //---custom event accessor that is invoked when client
        // subscribes to the event---
        event EventHandler<ILocationEventArgs>
            ILocation.locationObtained
        {
            add
            {
                locationObtained += value;
            }
            remove
            {
                locationObtained -= value;
            }
        }

        public void ObtainMyLocation()
        {
            lm = (LocationManager)
                Forms.Context.GetSystemService(
                    Context.LocationService);
            lm.RequestLocationUpdates(
                LocationManager.NetworkProvider,
                    0,   //---time in ms---
                    0,   //---distance in metres---
                    this);
        }

        ~GetMyLocation()
        {
            lm.RemoveUpdates(this);
        }
    }
}
