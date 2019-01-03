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
        public async Task<LocationInfo> GetCurrentLocation()
        {
            var position = await CrossGeolocator.Current.GetPositionAsync();

            return new LocationInfo { Latitude = position.Latitude, Longitude = position.Longitude };
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
            {
                return false;
            }

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }
    }
}
