using System.Collections.Generic;
using Windows.Devices.Geolocation;

namespace WlanDetection
{
    public class Signal
    {
        #region Fields
        #endregion

        #region Properties
        public Geoposition GeoPosition { get; set; }
        public List<WiFiSignal> WifiSignals { get; set; }
        #endregion
    }
}
