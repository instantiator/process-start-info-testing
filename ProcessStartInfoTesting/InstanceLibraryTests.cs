using System;
using Instances;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProcessStartInfoTesting
{
    [TestClass]
    public class InstanceLibraryTests
    {
        [TestMethod]
        public void TestInstanceCanRun()
        {
            var (exitCode, _) = Instance.Finish("ffmpeg", "-version");
            Assert.AreEqual(0, exitCode);
        }
    }
}
