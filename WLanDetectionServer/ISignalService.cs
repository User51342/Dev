using System.Collections.Generic;
using System.ServiceModel;

namespace WLanDetectionServer
{
    [ServiceContract]
    public interface ISignalService
    {
        [OperationContract]
        int SaveList(List<Signal> signals);
        [OperationContract]
        int Save(Signal signals);
    }
}
