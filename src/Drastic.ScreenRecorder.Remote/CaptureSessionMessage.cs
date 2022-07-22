// <copyright file="CaptureSessionMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public class CaptureSessionMessage
        : RemoteMessage
    {
        public CaptureSessionMessage()
            : base(RemoteMessageType.CaptureSession)
        {
        }

        public string TargetId { get; set; }

        public CaptureSessionFunction CaptureSessionFunction { get; set; }

        public TargetType TargetType { get; set; }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.TargetId = reader.ReadString();
            this.CaptureSessionFunction = (CaptureSessionFunction)reader.ReadUInt16();
            this.TargetType = (TargetType)reader.ReadUInt16();
        }

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteString(this.TargetId);
            writer.WriteUInt16((ushort)this.CaptureSessionFunction);
            writer.WriteUInt16((ushort)this.TargetType);
        }
    }

    public enum CaptureSessionFunction
        : ushort
    {
        Start,
        Stop
    }

    public enum TargetType
        : ushort
    {
        Display,
        Window,
    }
}
