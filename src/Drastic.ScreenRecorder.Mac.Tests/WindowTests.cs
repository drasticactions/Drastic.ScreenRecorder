// <copyright file="WindowTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ScreenRecorder.Tests;

namespace Drastic.ScreenRecorder.Mac.Tests
{
    [TestClass]
    public class WindowTests
    {
        [TestMethod]
        public void EnumerateWindows()
        {
            var windowEnumeration = new WindowEnumeration();
            SharedTests.WindowEnumeration(windowEnumeration);
        }

        [TestMethod]
        public void WindowCaptureContext()
        {
            var windowEnumeration = new WindowEnumeration();
            var window = SharedTests.WindowEnumeration(windowEnumeration) as WindowInfo;
            Assert.IsNotNull(window);

            var context = new WindowCapture(window);
            SharedTests.VerifyWindowSurface(context);
        }
    }
}
