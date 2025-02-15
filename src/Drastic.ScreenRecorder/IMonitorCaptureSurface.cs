﻿// <copyright file="IMonitorCaptureSurface.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.ScreenRecorder
{
    public interface IMonitorCaptureSurface
    {
        IMonitor Monitor { get; }

        ICaptureSurface Surface { get; }
    }
}
