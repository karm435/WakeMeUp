﻿using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using WakeMeUp.Services;
using WakeMeUp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WakeMeUp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private Position _currentPosition;
        private readonly ILocationService _locationService;
        private readonly IUserDialogs _userDialogs;

        public MainPageViewModel(ILocationService locationService, IUserDialogs userDialogs)
        {
            _locationService = locationService;
            _userDialogs = userDialogs;
        }

        public ICommand WhereAmI => new Command(async () => await WhereAmIAsked());

        private async Task WhereAmIAsked()
        {
            Debug.WriteLine("Where I am asked.");

            if (_locationService.IsLocationAvailable())
            {

                var location = await _locationService.GetCurrentLocation();
                if (location != null)
                {
                    // This is probably wrong. As it will create multiple objects of Postion which we do not want.
                    CurrentPosition = new Position(location.Latitude, location.Longitude);
                }
            }
            else
            {

                await _userDialogs.AlertAsync("Location is not enabled on the device.", "System Message", "Ok");
            }
        }


        public Position CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                if (value != _currentPosition)
                {
                    _currentPosition = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
