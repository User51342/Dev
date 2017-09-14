using AutoMapper;
using System.Collections.Generic;
using WlanDetection.SignalService;

namespace WlanDetection.Mapping
{
    public class WifiSignalsConvert : ITypeConverter<WiFiSignal, WifiSignalDto>
    {
        public WifiSignalDto Convert(WiFiSignal source, WifiSignalDto destination, ResolutionContext context)
        {
            return new WifiSignalDto()
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
        public IEnumerable<WifiSignalDto> Convert(List<WiFiSignal> source, List<WifiSignalDto> destination, ResolutionContext context)
        {
            return Mapper.Map<List<WiFiSignal>, List<WifiSignalDto>>(source);
        }
    }
}
