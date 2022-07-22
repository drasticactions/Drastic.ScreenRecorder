// <copyright file="ListMonitorsMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Drawing;
using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public class ListMonitorsMessage
        : RemoteMessage
    {
        public ListMonitorsMessage()
             : base(RemoteMessageType.ListMonitors)
        {
        }

        public IEnumerable<Monitor> Monitors { get; set; } = new List<Monitor>();

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteEnumerable<Monitor>(context, this.Monitors);
        }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.Monitors = reader.ReadEnumerable<Monitor>(context, new MonitorSerializer());
        }
    }

    public class MonitorSerializer : ISerializer<Monitor>
    {
        public Monitor Deserialize(ISerializationContext context, IValueReader reader)
        {
            var monitor = new Monitor();
            monitor.IsPrimary = reader.ReadBool();
            monitor.DeviceName = reader.ReadString();
            return monitor;
        }

        public void Serialize(ISerializationContext context, IValueWriter writer, Monitor element)
        {
            writer.WriteBool(element.IsPrimary);
            writer.WriteString(element.DeviceName);
        }
    }

    public class Monitor : ISerializable
    {
        public Monitor()
        {
        }

        public Monitor(IMonitor monitor)
        {
            this.IsPrimary = monitor.IsPrimary;
            this.DeviceName = monitor.DeviceName;
        }

        public bool IsPrimary { get; internal set; }

        public string DeviceName { get; internal set; }


        public void Deserialize(ISerializationContext context, IValueReader reader)
        {
            this.IsPrimary = reader.ReadBool();
            this.DeviceName = reader.ReadString();
        }

        public void Serialize(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteBool(this.IsPrimary);
            writer.WriteString(this.DeviceName);
        }
    }
}