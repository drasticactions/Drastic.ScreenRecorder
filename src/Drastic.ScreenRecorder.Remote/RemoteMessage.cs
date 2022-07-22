// <copyright file="RemoteMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public abstract class RemoteMessage
        : Message
    {
        protected RemoteMessage(RemoteMessageType type)
            : base(RemoteProtocol.Instance, (ushort)type)
        {
        }
    }
}