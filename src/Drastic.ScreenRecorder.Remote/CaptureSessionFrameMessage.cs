// <copyright file="CaptureSessionFrameMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public class CaptureSessionFrameMessage
        : RemoteMessage
    {
        public CaptureSessionFrameMessage()
            : base(RemoteMessageType.CaptureFrame)
        {
        }

        public string TargetId { get; set; }

        public byte[] ImageData { get; set; }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.TargetId = reader.ReadString();
            this.ImageData = reader.ReadBytes();
        }

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteString(this.TargetId);
            writer.WriteBytes(this.ImageData);
        }
    }
}
