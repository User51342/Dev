using WLanDetectionServer.Entities;

namespace WLanDetectionServer
{
    public class SignalService : ISignalService
    {
        public int Save(SignalDto signals)
        {
            var result = 0;
            using (var ctx = new SignalsContext())
            {
                ctx.Signals.Add(signals);
                result = ctx.SaveChanges();
            }
            return result;
        }
    }
}
