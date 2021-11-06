using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProcessStartInfoTesting
{
    [TestClass]
    public class UseShellExecuteTests
    {
        // According to: https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=net-5.0
        /*
         * When UseShellExecute is false, the FileName property can be
         * either a fully qualified path to the executable, or a simple
         * executable name that the system will attempt to find within
         * folders specified by the PATH environment variable.
         */

        private Process Launch(string filename, string argument, bool useShellExecute)
        {
            var info = new ProcessStartInfo()
            {
                FileName = filename,
                Arguments = argument,
                UseShellExecute = useShellExecute,
                CreateNoWindow = true
            };

            var process = new Process()
            {
                StartInfo = info,
                EnableRaisingEvents = true
            };

            process.Start();
            process.WaitForExit();

            return process;
        }

        private void TestLaunch(string filename, string argument, bool useShellExecute)
        {
            var process = Launch(filename, argument, useShellExecute);
            Assert.AreEqual(0, process.ExitCode);
        }

        [TestMethod]
        public void CanFind_ffmpeg_WithoutShellExecute()
        {
            Launch("ffmpeg", "-version", false);
        }

        [TestMethod]
        public void CanFind_ffmpeg_WithShellExecute()
        {
            Launch("ffmpeg", "-version", true);
        }

        [TestMethod]
        public void CanFind_docker_WithoutShellExecute()
        {
            Launch("docker", "--version", false);
        }

        [TestMethod]
        public void CanFind_docker_WithShellExecute()
        {
            Launch("docker", "--version", true);
        }

    }
}
