// <copyright file="MonitorTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ScreenRecorder.Tests;

namespace Drastic.ScreenRecorder.Mac.Tests
{
    [TestClass]
    public class MonitorTests
    {
        [TestMethod]
        public async Task EnumerateMonitors()
        {
            var monitorEnumeration = new MonitorEnumeration();
            var result = await monitorEnumeration.GetMonitorsAsync();
            SharedTests.MonitorEnumeration(monitorEnumeration);
        }

        [TestMethod]
        public void MonitorCaptureContext()
        {
            var monitorEnumeration = new MonitorEnumeration();
            var monitor = SharedTests.MonitorEnumeration(monitorEnumeration) as MonitorInfo;
            Assert.IsNotNull(monitor);

            var context = new MonitorCapture(monitor);
            SharedTests.VerifyMonitorSurface(context);
        }
    }
}
