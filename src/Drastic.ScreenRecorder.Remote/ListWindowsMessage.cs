// <copyright file="ListWindowsMessage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public class ListWindowsMessage
        : RemoteMessage
    {
        public ListWindowsMessage()
           : base(RemoteMessageType.ListWindows)
        {
        }

        public IEnumerable<Window> Windows { get; set; } = new List<Window>();

        public override void WritePayload(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteEnumerable<Window>(context, this.Windows);
        }

        public override void ReadPayload(ISerializationContext context, IValueReader reader)
        {
            this.Windows = reader.ReadEnumerable<Window>(context, new WindowSerializer());
        }
    }

    public class WindowSerializer : ISerializer<Window>
    {
        public Window Deserialize(ISerializationContext context, IValueReader reader)
        {
            var window = new Window();
            window.Title = reader.ReadString();
            return window;
        }

        public void Serialize(ISerializationContext context, IValueWriter writer, Window element)
        {
            writer.WriteString(element.Title);
        }
    }

    public class Window : ISerializable
    {
        public Window()
        {

        }

        public Window(IWindow window)
        {
            this.Title = window.Title;
        }

        /// <summary>
        /// Gets the window title.
        /// </summary>
        public string Title { get; internal set; }

        public void Deserialize(ISerializationContext context, IValueReader reader)
        {
            this.Title = reader.ReadString();
        }

        public void Serialize(ISerializationContext context, IValueWriter writer)
        {
            writer.WriteString(this.Title);
        }
    }
}
