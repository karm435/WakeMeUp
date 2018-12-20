using System.Threading.Tasks;
using WakeMeUp.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace WakeMeUp.Services
{
    public interface ILocationService
    {
        Task<LocationInfo> GetCurrentLocation();

        bool IsLocationAvailable();
    }
}