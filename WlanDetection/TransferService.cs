using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WlanDetection
{
    public class TransferService : ITransferService
    {
        public async Task<int> Save(Signal signal)
        {
            try
            {
                var tSignal = Mapper.Map<Signal, SignalService.Signal>(signal);
                var client = new SignalService.SignalServiceClient();
                var result = await client.SaveAsync(tSignal);
                return result;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveList(List<Signal> signals)
        {
            var tSignals = Mapper.Map<List<Signal>, ObservableCollection<SignalService.Signal>>(signals);
            var client = new SignalService.SignalServiceClient();
            var result = await client.SaveListAsync(tSignals);
            return result;
        }
    }
}
