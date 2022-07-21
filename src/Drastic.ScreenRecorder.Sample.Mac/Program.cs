// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ScreenRecorder.Mac;
using ScreenCaptureKit;

NSApplication.Init();

Console.WriteLine("Drastic.ScreenRecorder.Mac Sample");

//NSRunLoop.Main.InvokeOnMainThread(() => app.IterateMonitors());

NSRunLoop.Current.RunUntil(NSDate.DistantFuture);