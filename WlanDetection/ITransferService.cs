using System.Collections.Generic;
using System.Threading.Tasks;

namespace WlanDetection
{
    public interface ITransferService
    {
        Task<int> SaveList(List<Signal> signals);
        Task<int> Save(Signal signal);
    }
}
