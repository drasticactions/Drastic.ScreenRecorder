﻿// <copyright file="RequestItemsMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public class RequestItemsMessage
        : RemoteMessage
    {
        public RequestItemsMessage()
            : base(RemoteMessageType.RequestItems)
        {
        }

        public RemoteMessageType RequestedItem { get; set; }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
           this.RequestedItem = (RemoteMessageType)reader.ReadUInt16();
        }

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteUInt16((ushort)this.RequestedItem);
        }
    }
}