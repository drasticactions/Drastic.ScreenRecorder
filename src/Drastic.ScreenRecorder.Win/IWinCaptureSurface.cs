﻿// <copyright file="IWinCaptureSurface.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Windows.Graphics.Capture;

namespace Drastic.ScreenRecorder.Win
{
    public interface IWinCaptureSurface : ICaptureSurface
    {
        GraphicsCaptureItem? GraphicsCaptureItem { get; }
    }
}
