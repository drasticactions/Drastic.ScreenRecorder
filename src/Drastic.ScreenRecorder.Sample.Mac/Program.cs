// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ScreenRecorder;
using Drastic.ScreenRecorder.Mac;
using ScreenCaptureKit;

NSApplication.Init();

Console.WriteLine("Drastic.ScreenRecorder.Mac Sample");

var monitorEnumeration = new MonitorEnumeration();

NSRunLoop.Main.InvokeOnMainThread(async () =>
{
    var monitors = await monitorEnumeration.GetMonitorsAsync();
    var monitor = monitors.First() as MonitorInfo;
    if (monitor is null)
        return;

    var screenRecord = new ScreenRecorder(monitor.Display);
    screenRecord.CapturedFrame += OnCapturedFrame;
    screenRecord.StartCapture();
});

void OnCapturedFrame(object? sender, CapturedFrameEventArgs e)
{
    if (e.Frame is not null)
    {
        Console.WriteLine(e.Frame.Width);
    }

    //if (e.Frame.CGImage is CGImage image)
    //{
    //    var newRep = new NSBitmapImageRep(image);
    //    var nsData = newRep.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png);
    //    nsData.Save("test.png", true);
    //}
}

NSRunLoop.Current.RunUntil(NSDate.DistantFuture);