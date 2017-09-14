using AutoMapper;
using System;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WlanDetection
{
    public class TransferService : ITransferService
    {
        public async Task<int> Save(Signal signal)
        {
            try
            {
                var tSignal = Mapper.Map<Signal, SignalService.SignalDto>(signal);
                var client = new SignalService.SignalServiceClient();
                var result = await client.SaveAsync(tSignal);
                return result;
            }
            catch(CommunicationException ex)
            {
                signal.Errors.Add(ex.Message);
                return -1;
            }
            catch(Exception ex)
            {
                signal.Errors.Add("Can´t connect to server.");
                return -1;
            }
        }
    }
}
