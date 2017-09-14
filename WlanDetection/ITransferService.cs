using System.Threading.Tasks;

namespace WlanDetection
{
    public interface ITransferService
    {
        Task<int> CommitToServer(Signal signal);
        int GetSuspendedSignals();
        void Resume();
        void Suspend();
        

    }
}
