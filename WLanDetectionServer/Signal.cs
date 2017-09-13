using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WLanDetectionServer
{
    [DataContract]
   public class Signal
    {
        [DataMember]
        public Geoposition Geoposition { get; set; }
        [DataMember]
        public List<WifiSignal> WifiSignals { get; set; }
        [DataMember]
        public DateTime RecordTime { get; private set; }
    }
}
