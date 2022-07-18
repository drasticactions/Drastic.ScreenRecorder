// <copyright file="WindowCapture.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.ScreenRecorder.Mac
{
    public class WindowCapture : IWindowCaptureSurface
    {
        public WindowCapture(WindowInfo info)
        {
            this.Window = info;
            this.Surface = new MacCaptureItem(info);
        }

        public IWindow Window { get; }

        public ICaptureSurface Surface { get; }
    }
}
