using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WlanDetection.SignalService;

namespace WlanDetection.Mapping
{
    public class SignalConvert : ITypeConverter<Signal, SignalService.Signal>
    {
        public SignalService.Signal Convert(Signal source, SignalService.Signal destination, ResolutionContext context)
        {
            return new SignalService.Signal() {
                Geoposition = Mapper.Map< Windows.Devices.Geolocation.Geoposition,Geoposition>(source.Geoposition),
                RecordTime = source.RecordTime,
                WifiSignals = Mapper.Map <List<WiFiSignal>, ObservableCollection<SignalService.WifiSignal> > (source.WifiSignals) };
        }
    }
}
