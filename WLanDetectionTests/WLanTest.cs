
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WLanDetectionTests
{
    [TestClass]
    public class WLanTest
    {
        [TestMethod]
        public void ScanTest()
        {
            var wlan = new WlanDetection.WLan();
            wlan.Start();
        }
    }
}
