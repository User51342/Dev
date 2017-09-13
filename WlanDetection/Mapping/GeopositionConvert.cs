using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using WlanDetection.SignalService;

namespace WlanDetection.Mapping
{
    public class GeopositionConvert : ITypeConverter<Windows.Devices.Geolocation.Geoposition, SignalService.Geoposition>
    {
        public SignalService.Geoposition Convert(Windows.Devices.Geolocation.Geoposition source, SignalService.Geoposition destination, ResolutionContext context)
        {
            SignalService.Geoposition result = new SignalService.Geoposition();
            result.Longiture = source.Coordinate.Point.Position.Longitude;
            result.Latitude = source.Coordinate.Point.Position.Latitude;
            result.Altitude = source.Coordinate.Point.Position.Altitude;
            return result;
        }
    }
}
