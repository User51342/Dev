using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace WlanDetection.Detectors
{
    public class Scanner
    {
        #region Fields
        private List<WiFiSignal> _WiFiSignalsHistory = new List<WiFiSignal>();
        private DispatcherTimer timer;
        WLan _Wlan;
        #endregion

        #region Events
        public delegate void SignalChangeHandler(Signal signal);
        public event SignalChangeHandler ScanUpdated;
        #endregion

        #region Properties
        public bool IsGpsActive { get; set; }
        #endregion

        #region Public implementaions
        public void StartScanner()
        {
            _Wlan = new WLan();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(null, null);
        }

        public void StopScanner()
        {
            timer.Stop();
        }
        #endregion

        #region Private implementaions
        private async void Timer_Tick(object sender, object e)
        {
            await Scan();
        }

        private async Task Scan()
        {
            Signal signal = new Signal();
            List<WiFiSignal> wifiSignals = new List<WiFiSignal>();
            try
            {
                wifiSignals = await _Wlan.Scan();
            }
            catch (Exception wex)
            {
                signal.Errors.Add(wex.Message);
            }
            if (IsGpsActive)
            {
                var position = await Gps.GetPosition();
                signal.Geoposition = position;
            }
            signal.WifiSignals = wifiSignals;

            ScanUpdated(signal);

        }

        //protected virtual void OnScanUpdated(SignalEventArgs s)
        //{
        //    EventHandler handler = ScanUpdated;
        //    if(handler != null)
        //    {
        //        handler(this, s);
        //    }
        //}
        #endregion
    }
}
