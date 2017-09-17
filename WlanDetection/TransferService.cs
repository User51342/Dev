using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WlanDetection.SignalService;

namespace WlanDetection
{
    public class TransferService : ITransferService
    {
        #region Constants
        private const string _SuspendedFileName = "SaveSuspendedSignals.xml";
        #endregion

        #region Fields
        private List<SignalDto> _SuspendedSignals = new List<SignalDto>();
        #endregion

        #region Properties
        private List<SignalDto> SuspendedSignals
        {
            get { return _SuspendedSignals; }
            set { _SuspendedSignals = value; }
        }
        #endregion

        #region Public implementations
        public async Task<int> CommitToServer(Signal signal)
        {
            var signalDto = Mapper.Map<Signal, SignalService.SignalDto>(signal);
            try
            {
                var client = new SignalService.SignalServiceClient();
                var result = await client.SaveAsync(signalDto);
                if (_SuspendedSignals.Count > 0)
                {
                    Task.Run(() =>
                    {
                        TransferSuspended();
                    });
                }
                return result;
            }
            catch (CommunicationException ex)
            {
                signal.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                signal.Errors.Add("Can´t connect to server.");

            }

            SuspendedSignals.Add(signalDto);

            return -1;
        }



        public int GetSuspendedSignals()
        {
            return _SuspendedSignals.Count;
        }

        public void Suspend()
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var serializer = new XmlSerializer(typeof(List<SignalDto>));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            serializer.Serialize(sw, _SuspendedSignals);
            File.WriteAllText(Path.Combine(localFolder.Path, _SuspendedFileName), sb.ToString());
        }

        public void Resume()
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var serializer = new XmlSerializer(typeof(List<SignalDto>));

            var memoryStream = new MemoryStream();
            var sr = new StreamReader(memoryStream);
            if (File.Exists(Path.Combine(localFolder.Path, _SuspendedFileName)))
            {
                XmlDocument xmlDocument = new XmlDocument();
                var fileString = File.ReadAllText(Path.Combine(localFolder.Path, _SuspendedFileName));
                var stringReader = new StringReader(fileString);
                _SuspendedSignals = (List<SignalDto>)serializer.Deserialize(stringReader);
            }
        }
        #endregion

        #region Private implementations
        private void TransferSuspended()
        {
            var tDel = new List<SignalDto>();
            var tclient = new SignalService.SignalServiceClient();
            foreach (var tSignal in _SuspendedSignals)
            {
                try
                {
                    var tresult = tclient.SaveAsync(tSignal);
                    tDel.Add(tSignal);
                }
                catch
                { }
            }
            foreach (var tSignal in tDel)
            {
                _SuspendedSignals.Remove(tSignal);
            }
            tDel.Clear();
        }
        #endregion
    }
}
