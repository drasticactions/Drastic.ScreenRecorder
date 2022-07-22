// <copyright file="RemoteMessageType.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Drastic.ScreenRecorder.Remote
{
    public enum RemoteMessageType
        : ushort
    {
        RequestItems = 1,
        ListMonitors = 2,
        ListWindows = 3,
    }
}