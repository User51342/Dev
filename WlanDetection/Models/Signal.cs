using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;

namespace WlanDetection
{
    public class Signal
    {
        #region Fields
        private List<string> _Errors = new List<string>();
        #endregion

        #region Properties
        public Geoposition GeoPosition { get; set; }
        public List<WiFiSignal> WifiSignals { get; set; }
        public DateTime RecordTime { get; private set; }
        public List<string> Errors
        {
            get { return _Errors; }
            set { _Errors = value; }
        }
        #endregion

        #region Construction / Initialization / Deconstruction
        public Signal()
        {
            RecordTime = DateTime.Now;
        }
        #endregion
    }
}
