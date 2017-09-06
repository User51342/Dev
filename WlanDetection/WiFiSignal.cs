using System;
using Windows.Devices.Geolocation;

namespace WlanDetection
{
    public class WiFiSignal
    {
        #region Fields
        #endregion

        #region Properties
        public string MacAddress { get; set; }
        public string Ssid { get; set; }
        public string NetworkKind { get; set; }
        public string PhysicalKind { get; set; }
        public byte SignalBars { get; set; }
        public int ChannelCenterFrequencyInKilohertz { get; set; }
        public string NetworkEncryptionType { get; set; }
        public string Network​Authentication​Type { get; set; }
        public double NetworkRssiInDecibelMilliwatts { get; set; }
        public DateTime RecordTime { get; set; }
        public Geocoordinate GeoPosition { get; set; }
        #endregion

        #region Construction / Initialization / Deconstruction

        #endregion
    }
}