using System;
using System.Collections.Generic;

namespace WLanDetectionServer
{
    public class SignalService : ISignalService
    {
        public int Save(Signal signals)
        {
            return 1;
        }

        public int SaveList(List<Signal> signals)
        {
            return signals.Count;
        }
    }
}
