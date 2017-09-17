using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace WlanDetection.Detectors
{
    public class Gps
    {
       static Geolocator geolocator = null;
        private static  CancellationTokenSource _geolocationCancelationTokenSource = null;

        public async static Task<Geoposition> GetPosition()
        {
            if (await getLocationByGeolocatorAsync())
            { }

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



        private static async Task<bool> getLocationByGeolocatorAsync()
        {
            if (geolocator == null)
            {
                geolocator = new Geolocator();
            }

            bool boSuccess = false;

            //some preflight checks here
            if (geolocator.LocationStatus != PositionStatus.Disabled && geolocator.LocationStatus != PositionStatus.NotAvailable)
            {
                try
                {
                    // Get cancellation token 
                    _geolocationCancelationTokenSource = new CancellationTokenSource();
                    CancellationToken token = _geolocationCancelationTokenSource.Token;

                    //start internal timer to cancel geolocation before geolocator timeout will throw the uncatchable exception
                    cancelGeolocationTimerAsync();

                    Geoposition position = await geolocator.GetGeopositionAsync().AsTask(token);

                    //do some stuff with the result

                    boSuccess = true;
                }
                catch (Exception e) //<= that catch-bloack will only catch exceptions thrown on every code which is NOT the "await GetGeolocationAsync()"
                {
                    //do your exception handling here
                }
                finally
                {
                    _geolocationCancelationTokenSource = null;
                }
            }
            return boSuccess;
        }


        private static async Task cancelGeolocationTimerAsync()
        {
            //delay should be less than 7sec (cause 7sec is the default timeout of the geolocator)
            //if a delay > 7sec is needed a timer with a value set higher than the value here should 
            //be set to the geolocator manually
            await Task.Delay(2000);
            if (_geolocationCancelationTokenSource != null)
            {
                _geolocationCancelationTokenSource.Cancel();
                _geolocationCancelationTokenSource = null;
            }
        }
    }
}
