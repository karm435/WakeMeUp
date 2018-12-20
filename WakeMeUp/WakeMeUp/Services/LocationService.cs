using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using WakeMeUp.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace WakeMeUp.Services
{
    /// <summary>
    /// This service will provide the current location of the device. 
    /// Also checks for required permissions also.
    /// </summary>
    public class LocationService : ILocationService
    {
        //TODO: Use LocationModel to pass info. Bacuse if we started using diff lib it will be bound to xamarin.essentials location
        public async Task<LocationInfo> GetCurrentLocation()
        {
            //var lastKnowPosition = await Geolocation.GetLastKnownLocationAsync();
            //Debug.WriteLine(lastKnowPosition?.ToString() ?? "No last known location");

            //var location = await Geolocation.GetLocationAsync(new GeolocationRequest { DesiredAccuracy = GeolocationAccuracy.Best });
            //Debug.WriteLine(location?.ToString() ?? "No Location");

            var position = await CrossGeolocator.Current.GetPositionAsync();
            Debug.WriteLine(position?.ToString() ?? "no position");

            return new LocationInfo { Latitude = position.Latitude, Longitude = position.Longitude };
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.Current.IsGeolocationEnabled)
            {
                return false;
            }

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }
    }
}
