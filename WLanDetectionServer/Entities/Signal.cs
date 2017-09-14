using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WLanDetectionServer.Entities
{
    [Table("Signals")]
    [DataContract]
   public class SignalDto
    {
        [Key]
        public int SignalId { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longiture { get; set; }
        [DataMember]
        public double? Altitude { get; set; }
        [DataMember]
        public List<WifiSignalDto> WifiSignals { get; set; }
        [DataMember]
        public DateTime RecordTime { get; private set; }
    }
}
