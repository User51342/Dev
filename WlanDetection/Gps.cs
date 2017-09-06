using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace WlanDetection
{
    public class Gps
    {
        public async static Task<Geoposition> GetPosition()
        {
            Geoposition result = null;
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator();
                    result = await geolocator.GetGeopositionAsync();
                    break;
                case GeolocationAccessStatus.Denied:
                    break;
                case GeolocationAccessStatus.Unspecified:
                    break;
            }

            return result;
        }
    }
}
