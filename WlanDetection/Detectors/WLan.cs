using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.WiFi;

namespace WlanDetection.Detectors
{

    public class WLan
    {
        #region Fields
        private List<WiFiSignal> _WiFiSignals = new List<WiFiSignal>();
        #endregion

        #region Properties
        private WiFiAdapter _WiFiAdapter { get; set; }

        private List<WiFiSignal> WiFiSignals
        {
            get { return _WiFiSignals; }
            set { _WiFiSignals = value; }
        }
        #endregion

        #region Construction / Initialization / Deconstruction
        public WLan()
        {
        }
        #endregion

        #region Public implementations
        public async Task<List<WiFiSignal>> Scan()
        {try { 
            if (_WiFiAdapter == null)
            {
                await InitializeFirstAdapter();
            }
            await ScanForNetworks();
            }catch( Exception ex)
            {
                throw ex;
            }
            return WiFiSignals;
        }
        #endregion

        #region Private implementations
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
                    this._WiFiAdapter = await WiFiAdapter.FromIdAsync(wifiAdapterResults[0].Id);
                }
                else
                {
                    throw new Exception("WiFi Adapter not found.");
                }
            }
        }

        private async Task ScanForNetworks()
        {
            if (this._WiFiAdapter != null)
            {
                await this._WiFiAdapter.ScanAsync();
            }
            WiFiSignals.Clear();

            foreach (var availableNetwork in _WiFiAdapter.NetworkReport.AvailableNetworks)
            {
                var wifiSignal = new WiFiSignal()
                {
                    MacAddress = availableNetwork.Bssid,
                    Ssid = availableNetwork.Ssid,
                    NetworkKind = availableNetwork.NetworkKind.ToString(),
                    PhysicalKind = availableNetwork.PhyKind.ToString(),
                    SignalBars = availableNetwork.SignalBars,
                    ChannelCenterFrequencyInKilohertz = availableNetwork.ChannelCenterFrequencyInKilohertz,
                    NetworkEncryptionType = availableNetwork.SecuritySettings.NetworkEncryptionType.ToString(),
                    Network​Authentication​Type = availableNetwork.SecuritySettings.Network​Authentication​Type.ToString(),
                    NetworkRssiInDecibelMilliwatts = availableNetwork.NetworkRssiInDecibelMilliwatts
                };
                WiFiSignals.Add(wifiSignal);
            }
        }
        #endregion

    }
}
