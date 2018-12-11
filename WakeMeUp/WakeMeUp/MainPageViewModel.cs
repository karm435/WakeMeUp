using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace WakeMeUp
{
    public class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(isShowCurrentLocation)))
            {
                await HandleIsShowCurrentLocationPropertyChanged();
            }    
        }

        private async Task HandleIsShowCurrentLocationPropertyChanged()
        {
            if (IsShowCurrentLocation)
            {
                await GetPermission();
            }
        }

        private async System.Threading.Tasks.Task GetPermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                //if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Location))
                //{
                //   Add code to show a Display alert
                //}
                var result = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Location });
            }
        }

        private bool isShowCurrentLocation;

        public bool IsShowCurrentLocation
        {
            get { return isShowCurrentLocation; }
            set
            {
                isShowCurrentLocation = value;
                NotifyPropertyChanged();
            }
        }

    }
}
