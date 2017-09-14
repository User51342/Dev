using System.Collections.Generic;
using System.ServiceModel;
using WLanDetectionServer.Entities;

namespace WLanDetectionServer
{
    [ServiceContract]
    public interface ISignalService
    {
        [OperationContract]
        int Save(SignalDto signals);
    }
}
