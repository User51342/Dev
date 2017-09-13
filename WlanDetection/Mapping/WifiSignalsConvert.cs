using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlanDetection.SignalService;

namespace WlanDetection.Mapping
{
    public class WifiSignalsConvert : ITypeConverter<WiFiSignal, SignalService.WifiSignal>
    {
        public WifiSignal Convert(WiFiSignal source, WifiSignal destination, ResolutionContext context)
        {
            return new WifiSignal()
            {
                ChannelCenterFrequencyInKilohertz = source.ChannelCenterFrequencyInKilohertz,
                MacAddress = source.MacAddress,
                NetworkAuthenticationType = source.NetworkAuthenticationType,
                NetworkEncryptionType = source.NetworkEncryptionType,
                NetworkKind = source.NetworkKind,
                NetworkRssiInDecibelMilliwatts = source.NetworkRssiInDecibelMilliwatts,
                PhysicalKind = source.PhysicalKind,
                SignalBars = source.SignalBars,
                Ssid = source.Ssid
            };
        }
        public IEnumerable<WifiSignal> Convert(List<WiFiSignal> source, List<WifiSignal> destination, ResolutionContext context)
        {
            return Mapper.Map<List<WiFiSignal>, List<SignalService.WifiSignal>>(source);
        }
    }
}
