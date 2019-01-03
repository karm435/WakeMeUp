using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using WakeMeUp.Models;
using WakeMeUp.Services;
using WakeMeUp.ViewModels.Base;
using Xamarin.Forms;

namespace WakeMeUp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private LocationInfo _currentPosition;
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
            if (_locationService.IsLocationAvailable())
            {
                CurrentPosition = await _locationService.GetCurrentLocation();
            }
            else
            {
                await _userDialogs.AlertAsync("Location is not enabled on the device.", "System Message", "Ok");
            }
        }

        public LocationInfo CurrentPosition
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
