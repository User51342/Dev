using System.Data.Entity;
using WLanDetectionServer.Entities;

namespace WLanDetectionServer
{
    public class SignalsContext : DbContext
    {
        public DbSet<SignalDto> Signals { get; set; }
        public DbSet<WifiSignalDto> WifiSignals { get; set; }
    }
}
