// <copyright file="RemoteProtocol.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public static class RemoteProtocol
    {
        public static readonly Protocol Instance = new Protocol(42, 1);

        static RemoteProtocol()
        {
            Instance.Discover();
        }
    }
}