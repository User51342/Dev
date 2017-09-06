using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace WlanDetection
{
    public class Scanner
    {
        #region Fields
        private List<WiFiSignal> _WiFiSignalsHistory = new List<WiFiSignal>();
        private DispatcherTimer timer;
        WLan _Wlan;
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
            var wifiSignals = await _Wlan.Scan();
            var position = Gps.GetPosition();
            Signal signal = new Signal()
            {
                GeoPosition = position.Result,
                WifiSignals = wifiSignals
            };
        }
        #endregion
    }
}
