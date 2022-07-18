// <copyright file="IMonitorEnumeration.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.ScreenRecorder
{
    public interface IMonitorEnumeration
    {
        IReadOnlyList<IMonitor> GetMonitors();

        Task<IReadOnlyList<IMonitor>> GetMonitorsAsync();
    }
}