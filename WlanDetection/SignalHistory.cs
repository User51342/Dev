using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlanDetection
{
    public class SignalHistory
    {
        public DateTime HistoryDateTime { get; set; }
        public byte SignalBars { get; set; }
        public int ChannelCenterFrequencyInKilohertz { get; set; }
        public string NetworkEncryptionType { get; set; }
        public string Network​Authentication​Type { get; set; }
        public double NetworkRssiInDecibelMilliwatts { get; set; }
    }
}
