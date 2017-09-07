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
        public ScannerViewModel()
        {
            ScanStartCommand = new RelayCommand(ExecuteScanStartCommand);
        }
        #endregion

        #region Private implementations
        private void ExecuteScanStartCommand()
        {
            ExecuteScan();
        }

        private void ExecuteScan()
        {
            if (_Scanner == null)
            {
                _Scanner = new Scanner();
                _Scanner.ScanUpdated += OnScanUpdated; ;
            }
            Scanning = !Scanning;
            if (Scanning)
            {
                _Scanner.StartScanner();
            }
            else
            {
                _Scanner.StopScanner();
            }
        }

        private void OnScanUpdated(Signal signal)
        {
            WifiSignals.Clear();
            if (signal.WifiSignals.Count() > 0)
            {
                foreach (var s in signal.WifiSignals.OrderByDescending(s => s.NetworkRssiInDecibelMilliwatts))
                {
                    WifiSignals.Add(s);
                }

                BasicGeoposition basicGeoPosition = new BasicGeoposition() { Latitude = signal.GeoPosition.Coordinate.Latitude, Longitude = signal.GeoPosition.Coordinate.Longitude };
                CurrentLocation = new Geopoint(basicGeoPosition);// signal.GeoPosition.Coordinate.Point;

                OutputText = $"Latitude: {signal.GeoPosition.Coordinate.Latitude.ToString()}, Longitude: {signal.GeoPosition.Coordinate.Longitude.ToString()}";
            }
            else
            {
                OutputText = "No Wifi signals." + Environment.NewLine;
            }

            foreach (var e in signal.Errors)
            {
                OutputText += e + Environment.NewLine;
            }
        }
        #endregion
    }
}
