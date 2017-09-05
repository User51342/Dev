using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.WiFi;

namespace WlanDetection
{

    public class WLan
    {
        #region Fields
        private List<WiFiSignal> _WiFiSignals = new List<WiFiSignal>();
        #endregion

        #region Properties
        private WiFiAdapter WiFiAdapter { get; set; }

        private List<WiFiSignal> WiFiSignals
        {
            get { return _WiFiSignals; }
            set { _WiFiSignals = value; }
        }

        #endregion

        #region Construction / Initialization / Deconstruction
        public WLan()
        {
            Init();
        }
        #endregion

        #region Public implementations

        public async Task<List<WiFiSignal>> Scan()
        {

            await ScanForNetworks();
            return WiFiSignals;
        }
        #endregion

        #region Private implementations
        private async void Init()
        {
            await InitializeFirstAdapter();
        }

        private async Task InitializeFirstAdapter()
        {
            var access = await WiFiAdapter.RequestAccessAsync();
            if (access != WiFiAccessStatus.Allowed)
            {
                throw new Exception("WiFiAccessStatus not allowed");
            }
            else
            {
                var wifiAdapterResults = await DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
                if (wifiAdapterResults.Count >= 1)
                {
                    this.WiFiAdapter = await WiFiAdapter.FromIdAsync(wifiAdapterResults[0].Id);
                }
                else
                {
                    throw new Exception("WiFi Adapter not found.");
                }
            }
        }

        private async Task ScanForNetworks()
        {
            if (this.WiFiAdapter != null)
            {
                await this.WiFiAdapter.ScanAsync();
            }

            foreach (var availableNetwork in WiFiAdapter.NetworkReport.AvailableNetworks)
            {
                WiFiSignal wifiSignal = WiFiSignals.FirstOrDefault(w => w.MacAddress == availableNetwork.Bssid);
                if (wifiSignal != null)
                {
                    wifiSignal.SignalHistories.Add(new SignalHistory()
                    {
                        HistoryDateTime = DateTime.Now,
                        SignalBars = availableNetwork.SignalBars,
                        ChannelCenterFrequencyInKilohertz = availableNetwork.ChannelCenterFrequencyInKilohertz,
                        NetworkEncryptionType = availableNetwork.SecuritySettings.NetworkEncryptionType.ToString(),
                        Network​Authentication​Type = availableNetwork.SecuritySettings.Network​Authentication​Type.ToString(),
                        NetworkRssiInDecibelMilliwatts = availableNetwork.NetworkRssiInDecibelMilliwatts,
                    });
                }
                else
                {
                    wifiSignal = new WiFiSignal()
                    {
                        MacAddress = availableNetwork.Bssid,
                        Ssid = availableNetwork.Ssid,
                        NetworkKind = availableNetwork.NetworkKind.ToString(),
                        PhysicalKind = availableNetwork.PhyKind.ToString(),
                    };
                    wifiSignal.SignalHistories.Add(new SignalHistory()
                    {
                        HistoryDateTime = DateTime.Now,
                        SignalBars = availableNetwork.SignalBars,
                        ChannelCenterFrequencyInKilohertz = availableNetwork.ChannelCenterFrequencyInKilohertz,
                        NetworkEncryptionType = availableNetwork.SecuritySettings.NetworkEncryptionType.ToString(),
                        Network​Authentication​Type = availableNetwork.SecuritySettings.Network​Authentication​Type.ToString(),
                        NetworkRssiInDecibelMilliwatts = availableNetwork.NetworkRssiInDecibelMilliwatts,
                    });
                }
                WiFiSignals.Add(wifiSignal);
            }
        }
        #endregion

    }
}
