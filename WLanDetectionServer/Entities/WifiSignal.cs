using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WLanDetectionServer.Entities
{
    [Table("WifiSignals")]
    public class WifiSignalDto
    {
        #region Properties
        [Key]
        public int WifiSignalId { get; set; }
        public string MacAddress { get; set; }
        public string Ssid { get; set; }
        public string NetworkKind { get; set; }
        public string PhysicalKind { get; set; }
        public byte SignalBars { get; set; }
        public int ChannelCenterFrequencyInKilohertz { get; set; }
        public string NetworkEncryptionType { get; set; }
        public string Network​Authentication​Type { get; set; }
        public double NetworkRssiInDecibelMilliwatts { get; set; }
        #endregion
    }
}
