using NUnit.Framework;
using System.Diagnostics;

namespace AGDATA.ApiTests
{
    [SetUpFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Trace.Flush();
        }
    }
}
