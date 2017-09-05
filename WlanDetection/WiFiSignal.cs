using System.Collections.Generic;

namespace WlanDetection
{
    public class WiFiSignal
    {
        #region Fields
        private List<SignalHistory> signalHistory;
        #endregion
        #region Properties
        public string MacAddress { get; set; }
        public string Ssid { get; set; }
        public string NetworkKind { get; set; }
        public string PhysicalKind { get; set; }

        public List<SignalHistory> SignalHistories
        {
            get
            {
                return signalHistory;
            }
            set
            {
                signalHistory = value;
            }
        }
        #endregion

        #region Construction / Initialization / Deconstruction
        public WiFiSignal()
        {
            signalHistory = new List<WlanDetection.SignalHistory>();
        }
        #endregion
    }
}