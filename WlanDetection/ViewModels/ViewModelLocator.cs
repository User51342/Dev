using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace WlanDetection.ViewModels
{
    public class ViewModelLocator
    {
        #region Construction / Initialization / Deconstruction
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            else
            {
                // Create run time view services and models
            }

            //Register your services used here
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<ScannerViewModel>();
        }
        #endregion

        #region Properties
        public ScannerViewModel ScannerViewInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScannerViewModel>();
            }
        }
        #endregion
    }
}
