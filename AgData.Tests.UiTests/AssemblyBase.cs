using Framework;
using NUnit.Framework;
using System.Diagnostics;

namespace Tests
{
    [SetUpFixture]
    public class AssemblyBase
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            FW.SetConfig();
            FW.CreateTestResultsDirectory();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Trace.Flush();
        }
    }
}
