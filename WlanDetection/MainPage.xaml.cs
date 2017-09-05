using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WlanDetection
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields
        private WLan _Wlan;
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            _Wlan = new WLan();
        }

        private async void btnScan_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(null, null);
        }
        private async void Timer_Tick(object sender, object e)
        {
            var wifiSignals = await _Wlan.Scan();
            if(wifiSignals.Count() > 0)
            {
                txbReport.Text = GetReport(wifiSignals);
            }
            else
            {
                txbReport.Text = "No networks found. Is wifi activated?";
            }
            
        }

            private string GetReport(List<WiFiSignal> wifiSignals)
        {
            StringBuilder builder = new StringBuilder();
            foreach(var wifi in wifiSignals.OrderByDescending(r => r.SignalBars))
            {
                builder.AppendLine($"{wifi.Ssid} | {wifi.NetworkEncryptionType} | {wifi.Network​Authentication​Type} | {wifi.SignalBars} | {wifi.NetworkRssiInDecibelMilliwatts} ");
            }
            return builder.ToString();
            
        }
    }
}
