using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

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
        private Scanner _Worker;
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

        #endregion

        #region Construction / Initialization / Deconstruction
        public ScannerViewModel()
        {
            ScanStartCommand = new RelayCommand(ExecuteScanStartCommand);
            _Worker = new Scanner();
        }
        #endregion

        #region Private implementations
        private void ExecuteScanStartCommand()
        {
            ExecuteScan();
        }

        private void ExecuteScan()
        {
            Scanning = !Scanning;
            if (Scanning)
            {

                _Worker.StartScanner();
            }
            else
            {
                _Worker.StopScanner();
            }
        }
        #endregion
    }
}
