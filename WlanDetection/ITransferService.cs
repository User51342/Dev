using System.Threading.Tasks;

namespace WlanDetection
{
    public interface ITransferService
    {
        Task<int> Save(Signal signal);
    }
}
