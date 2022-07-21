// <copyright file="MonitorTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ScreenRecorder.Tests;

namespace Drastic.ScreenRecorder.Win.Tests
{
    [TestClass]
    public class MonitorTests
    {
        [TestMethod]
        public void EnumerateMonitors()
        {
            var monitorEnumeration = new MonitorEnumeration();
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

        [TestMethod]
        public async Task MonitorCaptureSession()
        {
            var monitorEnumeration = new MonitorEnumeration();
            var monitor = SharedTests.MonitorEnumeration(monitorEnumeration) as MonitorInfo;
            Assert.IsNotNull(monitor);

            var context = new MonitorCapture(monitor);
            if (context.Surface is IWinCaptureSurface surface)
            {
                var session = new CaptureSession(surface);
                await SharedTests.VerifyCaptureSession(session);
            }
            else
            {
                Assert.Inconclusive("Failed to get IWinCaptureSurface");
                return;
            }
        }
    }
}
