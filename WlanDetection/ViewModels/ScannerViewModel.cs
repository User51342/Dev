using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Devices.Geolocation;
using WlanDetection.Detectors;

namespace WlanDetection.ViewModels
{
    public class ScannerViewModel : ViewModelBase
    {
        #region Constants
        const string StartScanText = "Start scanning";
        const string StopScanText = "Stop scanning";
        #endregion

        #region Fields
        private string _ActualCoordinates;
        private string _StartScanButtonText = StartScanText;
        private bool _scanning;
        private Scanner _Scanner;
        private ObservableCollection<WiFiSignal> _WifiSignals = new ObservableCollection<WiFiSignal>();
        private string _OutputText;
        private Geopoint _CurrentLocation;
        private ITransferService _TransferService;
        private RelayCommand _GpsActiveCommand;
        private bool _IsGpsActive;
        #endregion

        #region Properties
        public RelayCommand ScanStartCommand { get; private set; }

        public string ActualCoordinates
        {
            get
            {
                return _ActualCoordinates;
            }
            set
            {
                _ActualCoordinates = value;
                RaisePropertyChanged("ActualCoordinates");
            }
        }

        public RelayCommand GpsActiveCommand
        {
            get
            {
                return _GpsActiveCommand;
            }
            set
            {
                _GpsActiveCommand = value;
                RaisePropertyChanged("GpsActiveCommand");
            }
        }
        private bool IsGpsActive
        {
            get { return _IsGpsActive; }
            set
            {
                _IsGpsActive = value;
                RaisePropertyChanged("IsGpsActive");
            }
        }

        public string OutputText
        {
            get { return _OutputText; }
            set
            {
                _OutputText = value;
                RaisePropertyChanged("OutputText");
            }
        }

        public string StartScanButtonText
        {
            get { return _StartScanButtonText; }
            set
            {
                _StartScanButtonText = value;
                RaisePropertyChanged("StartScanButtonText");
            }
        }

        public bool Scanning
        {
            get { return _scanning; }
            set
            {
                if (_scanning != value)
                {
                    _scanning = value;
                    if (_scanning)
                    {
                        StartScanButtonText = StopScanText;
                    }
                    else
                    {
                        StartScanButtonText = StartScanText;
                    }
                    RaisePropertyChanged("Scanning");
                }
            }
        }

        public ObservableCollection<WiFiSignal> WifiSignals
        {
            get { return _WifiSignals; }
            set { _WifiSignals = value; }
        }

        public Geopoint CurrentLocation
        {
            get
            {
                return _CurrentLocation;
            }
            set
            {
                _CurrentLocation = value;
                RaisePropertyChanged("CurrentLocation");
            }
        }
        #endregion

        #region Construction / Initialization / Deconstruction
        public ScannerViewModel(ITransferService transferService)
        {
            ScanStartCommand = new RelayCommand(ExecuteScanStartCommand);
            GpsActiveCommand = new RelayCommand(ExecuteGpsActiveCommand);
            _TransferService = transferService;
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                WifiSignals.Add(new WiFiSignal() { Ssid = "First net", ChannelCenterFrequencyInKilohertz = 1, NetworkRssiInDecibelMilliwatts = -12d, NetworkKind = "Ccmp" });
                WifiSignals.Add(new WiFiSignal() { Ssid = "Second net", ChannelCenterFrequencyInKilohertz = 1, NetworkRssiInDecibelMilliwatts = -33, NetworkKind = "Ccmp" });
                WifiSignals.Add(new WiFiSignal() { Ssid = "Third net", ChannelCenterFrequencyInKilohertz = 1, NetworkRssiInDecibelMilliwatts = -54, NetworkKind = "Ccmp" });
                WifiSignals.Add(new WiFiSignal() { Ssid = "Fourth net", ChannelCenterFrequencyInKilohertz = 1, NetworkRssiInDecibelMilliwatts = -64, NetworkKind = "Ccmp" });
                WifiSignals.Add(new WiFiSignal() { Ssid = "Fiveth net", ChannelCenterFrequencyInKilohertz = 1, NetworkRssiInDecibelMilliwatts = -66, NetworkKind = "Ccmp" });
                WifiSignals.Add(new WiFiSignal() { Ssid = "Sixth net", ChannelCenterFrequencyInKilohertz = 1, NetworkRssiInDecibelMilliwatts = -67, NetworkKind = "Ccmp" });
            }
        }
        #endregion

        #region Private implementations
        private void ExecuteScanStartCommand()
        {
            ExecuteScan();
        }

        private void ExecuteGpsActiveCommand()
        {
            IsGpsActive = !IsGpsActive;
            if (_Scanner != null)
            {
                _Scanner.IsGpsActive = IsGpsActive;
            }
        }

        private void ExecuteScan()
        {
            if (_Scanner == null)
            {
                _Scanner = new Scanner();
                _Scanner.IsGpsActive = IsGpsActive;
                _Scanner.ScanUpdated += OnScanUpdated;
            }
            Scanning = !Scanning;
            if (Scanning)
            {
                _Scanner.StartScanner();
            }
            else
            {
                _Scanner.StopScanner();
                WifiSignals.Clear();
                OutputText = string.Empty;
                ActualCoordinates = string.Empty;
            }
        }

        private async void OnScanUpdated(Signal signal)
        {
            WifiSignals.Clear();
            OutputText = string.Empty;
            if (signal.WifiSignals.Count() > 0)
            {
                foreach (var s in signal.WifiSignals.OrderByDescending(s => s.NetworkRssiInDecibelMilliwatts))
                {
                    WifiSignals.Add(s);
                }

                //  BasicGeoposition basicGeoPosition = new BasicGeoposition() { Latitude = signal.Geoposition.Coordinate.Point.Position.Latitude, Longitude = signal.Geoposition.Coordinate.Point.Position.Longitude };
                //  CurrentLocation = new Geopoint(basicGeoPosition);// signal.GeoPosition.Coordinate.Point;

                ActualCoordinates = $"Latitude: {signal.Geoposition.Coordinate.Point.Position.Latitude.ToString()}, Longitude: {signal.Geoposition.Coordinate.Point.Position.Longitude.ToString()}";
                await _TransferService.CommitToServer(signal);
            }
            else
            {
                OutputText = "No Wifi signals."; // + Environment.NewLine;
            }

            foreach (var e in signal.Errors)
            {
                OutputText += e; // + Environment.NewLine;
            }
            OutputText += string.Format($"(S: {_TransferService.GetSuspendedSignals()})");
        }
        #endregion
    }
}
