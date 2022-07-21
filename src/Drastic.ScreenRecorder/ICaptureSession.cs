// <copyright file="ICaptureSession.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.ScreenRecorder
{
    public interface ICaptureSession
    {
        event EventHandler<CapturedFrameEventArgs>? OnCapturedFrame;

        void Start();

        void Stop();
    }
}
