using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WlanDetection.SignalService;

namespace WlanDetection.Mapping
{
    public class SignalConvert : ITypeConverter<Signal, SignalDto>
    {
        public SignalDto Convert(Signal source, SignalDto destination, ResolutionContext context)
        {
            return new SignalDto()
            {
                Altitude = source.Geoposition.Coordinate.Point.Position.Altitude,
                Latitude = source.Geoposition.Coordinate.Point.Position.Latitude,
                Longiture = source.Geoposition.Coordinate.Point.Position.Longitude,
                RecordTime = source.RecordTime,
                WifiSignals = Mapper.Map<List<WiFiSignal>, ObservableCollection<WifiSignalDto>>(source.WifiSignals)
            };
        }
    }
}
