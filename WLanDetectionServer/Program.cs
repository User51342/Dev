using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WLanDetectionServer
{
    class Program
    {
        static void Main(string[] args)
        {

            Uri uri = new Uri(@"http://127.0.0.1:81/wlandetectionservice/");
            using (ServiceHost host = new ServiceHost(typeof(SignalService), uri))
            {
                host.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
                host.Open();
                Console.WriteLine("Connection open. (Press key to close)");
                Console.ReadLine();
                host.Close();
                Console.WriteLine("Connection closed.");
            }

        }
    }
}
