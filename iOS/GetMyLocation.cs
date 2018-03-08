using System;
using CoreLocation;
using WhetherPOC.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(GetMyLocation))]
namespace WhetherPOC.iOS
{

    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class GetMyLocation: ILocation
    {
        CLLocationManager lm;
        string status;

        public event EventHandler<ILocationEventArgs> locationObtained;

        public void ObtainMyLocation()
        {
            lm = new CLLocationManager();
            lm.DesiredAccuracy = CLLocation.AccuracyBest;
            lm.DistanceFilter = CLLocationDistance.FilterNone;
            //---fired whenever there is a change in location---
            lm.LocationsUpdated +=
                (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    var locations = e.Locations;
                    var strLocation =
                        locations[locations.Length - 1].
                            Coordinate.Latitude.ToString();
                    strLocation = strLocation + "," +
                                        locations[locations.Length - 1].
                                            Coordinate.Longitude.ToString();
                    LocationEventArgs args = new LocationEventArgs();
                    args.lat = locations[locations.Length - 1].
                                        Coordinate.Latitude;
                    args.lng = locations[locations.Length - 1].
                        Coordinate.Longitude;

                    locationObtained(this, args);
                };
            lm.AuthorizationChanged += (object sender,
                CLAuthorizationChangedEventArgs e) =>
            {
                if (e.Status ==
                    CLAuthorizationStatus.AuthorizedWhenInUse)
                {
                    Xamarin.Forms.Application.Current.Properties["LocationEnable"] = true;
                    lm.StartUpdatingLocation();
                }
                if (e.Status == CLAuthorizationStatus.AuthorizedAlways)
                {
                    Xamarin.Forms.Application.Current.Properties["LocationEnable"] = true;
                    lm.StartUpdatingLocation();
                }

                if (e.Status == CLAuthorizationStatus.Denied)
                {
                    Xamarin.Forms.Application.Current.Properties["LocationEnable"] = false;
                }


            };
            lm.RequestWhenInUseAuthorization();
        }

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
        ~GetMyLocation()
        {
            lm.StopUpdatingLocation();
        }


    }
}