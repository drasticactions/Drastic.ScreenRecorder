﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drastic.ScreenRecorder.Tests
{
    public static class SharedTests
    {
        public static IMonitor MonitorEnumeration(IMonitorEnumeration monitorEnumeration)
        {
            var monitors = monitorEnumeration.GetMonitors();
            Assert.IsNotNull(monitors);
            Assert.IsTrue(monitors.Count > 0);

            var monitor = monitors.First();

            Assert.IsTrue(!string.IsNullOrEmpty(monitor.DeviceName));

            return monitor;
        }

        public static void VerifyCaptureSurface(ICaptureSurface? surface)
        {
            Assert.IsNotNull(surface);
            Assert.IsNotNull(surface.RawSurface);
        }

        public static void VerifyMonitorSurface(IMonitorCaptureSurface? monitor)
        {
            Assert.IsNotNull(monitor?.Monitor);
            VerifyCaptureSurface(monitor?.Surface);
        }

        public static IWindow WindowEnumeration(IWindowEnumeration windowEnumeration)
        {
            var windows = windowEnumeration.GetWindows();
            Assert.IsNotNull(windows);
            Assert.IsTrue(windows.Count > 0);

            var window = windows.First(n => !string.IsNullOrEmpty(n.Title));

            return window;
        }

        public static void VerifyWindowSurface(IWindowCaptureSurface? window)
        {
            Assert.IsNotNull(window?.Window);
            VerifyCaptureSurface(window?.Surface);
        }

        public static async Task<ICapturedFrame> VerifyCaptureSession(ICaptureSession session)
        {
            ICapturedFrame? frame = null;

            session.OnCapturedFrame += Session_OnCapturedFrame;

            session.Start();

            await Task.Delay(1000);

            Assert.IsNotNull(frame);

            void Session_OnCapturedFrame(object? sender, CapturedFrameEventArgs e)
            {
                frame = e.Frame;
                session.Stop();
            }

            return frame;
        }
    }
}
