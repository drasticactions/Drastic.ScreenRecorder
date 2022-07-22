// <copyright file="RemoteServer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tempest;
using System.Collections.Concurrent;

namespace Drastic.ScreenRecorder.Remote
{
    public sealed class RemoteServer
        : TempestServer
    {
        private readonly List<IConnection> connections = new List<IConnection>();

        private IMonitorEnumeration monitorEnumeration;
        private IWindowEnumeration windowEnumeration;
        private ICaptureSessionFactory factory;
        private IFrameEncoder frameEncoder;

        private List<ConnectionCaptureSession> sessions = new List<ConnectionCaptureSession>();

        public RemoteServer(IFrameEncoder frameEncoder, ICaptureSessionFactory factory, IMonitorEnumeration monitor, IWindowEnumeration window, IConnectionProvider provider)
            : base(provider, MessageTypes.Reliable)
        {
            this.frameEncoder = frameEncoder;
            this.factory = factory;
            this.monitorEnumeration = monitor;
            this.windowEnumeration = window;
            this.RegisterMessageHandler<RequestItemsMessage>(this.OnRequestItemsMessage);
            this.RegisterMessageHandler<CaptureSessionMessage>(this.OnCaptureSessionMessage);
        }

        private async void OnCaptureSessionMessage(MessageEventArgs<CaptureSessionMessage> obj)
        {
            switch (obj.Message.TargetType)
            {
                case TargetType.Display:
                    await this.HandleDisplayCaptureSessionAsync(obj.Connection, obj.Message);
                    break;
                case TargetType.Window:
                    await this.HandleWindowCaptureSessionAsync(obj.Connection, obj.Message);
                    break;
                default:
                    // TODO: Send Error message.
                    break;
            }
        }

        private async Task HandleDisplayCaptureSessionAsync(IConnection connection, CaptureSessionMessage message)
        {
            var session = this.GetCaptureSession(connection, message);
            if (session is null)
            {
                var monitor = (await this.monitorEnumeration.GetMonitorsAsync()).FirstOrDefault(n => n.DeviceName == message.TargetId);
                if (monitor is null)
                {
                    // TODO: Send Error Message.
                    return;
                }

                session = new ConnectionCaptureSession(connection, this.frameEncoder, this.factory.CreateCaptureSessionForMonitor(monitor));
                this.sessions.Add(session);
            }

            await this.HandleCaptureSessionFunction(session, message.CaptureSessionFunction);
        }

        private async Task HandleWindowCaptureSessionAsync(IConnection connection, CaptureSessionMessage message)
        {
            var session = this.GetCaptureSession(connection, message);
            if (session is null)
            {
                var window = (await this.windowEnumeration.GetWindowsAsync()).FirstOrDefault(n => n.Title == message.TargetId);
                if (window is null)
                {
                    // TODO: Send Error Message.
                    return;
                }

                session = new ConnectionCaptureSession(connection, this.frameEncoder, this.factory.CreateCaptureSessionForWindow(window));
                this.sessions.Add(session);
            }

            await this.HandleCaptureSessionFunction(session, message.CaptureSessionFunction);
        }

        private async Task HandleCaptureSessionFunction(ConnectionCaptureSession session, CaptureSessionFunction function)
        {
            switch (function)
            {
                case CaptureSessionFunction.Start:
                    await session.Start();
                    break;
                case CaptureSessionFunction.Stop:
                    await session.Stop();
                    break;
                default:
                    // TODO: Send Error Message.
                    break;
            }
        }

        private ConnectionCaptureSession? GetCaptureSession(IConnection connection, CaptureSessionMessage message)
            => this.sessions.FirstOrDefault(n => n.Connection == connection && n.Session.Title == message.TargetId);

        private void OnRequestItemsMessage(MessageEventArgs<RequestItemsMessage> obj)
        {
            switch (obj.Message.RequestedItem)
            {
                case RemoteMessageType.ListMonitors:
                    this.SendMonitorRequestListAsync(obj.Connection);
                    break;
                case RemoteMessageType.ListWindows:
                    this.SendWindowsRequestListAsync(obj.Connection);
                    break;
            }
        }

        /// <inheritdoc/>
        protected override void OnConnectionMade(object sender, ConnectionMadeEventArgs e)
        {
            lock (this.connections)
            {
                this.connections.Add(e.Connection);
            }

            base.OnConnectionMade(sender, e);
        }

        /// <inheritdoc/>
        protected override void OnConnectionDisconnected(object sender, DisconnectedEventArgs e)
        {
            lock (this.connections)
            {
                this.connections.Remove(e.Connection);
            }

            base.OnConnectionDisconnected(sender, e);
        }

        private async Task SendMonitorRequestListAsync(IConnection connection)
        {
            var monitors = await this.monitorEnumeration.GetMonitorsAsync();
            await connection.SendAsync(new ListMonitorsMessage() { Monitors = monitors.Select(n => new Monitor(n)) });
        }

        private async Task SendWindowsRequestListAsync(IConnection connection)
        {
            var windows = await this.windowEnumeration.GetWindowsAsync();
            await connection.SendAsync(new ListWindowsMessage() { Windows = windows.Select(n => new Window(n)) });
        }
    }
}