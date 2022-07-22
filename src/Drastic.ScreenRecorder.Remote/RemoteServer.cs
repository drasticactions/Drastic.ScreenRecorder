// <copyright file="RemoteServer.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public sealed class RemoteServer
        : TempestServer
    {
        private readonly List<IConnection> connections = new List<IConnection>();

        private IMonitorEnumeration monitorEnumeration;
        private IWindowEnumeration windowEnumeration;

        public RemoteServer(IMonitorEnumeration monitor, IWindowEnumeration window, IConnectionProvider provider)
            : base(provider, MessageTypes.Reliable)
        {
            this.monitorEnumeration = monitor;
            this.windowEnumeration = window;
            this.RegisterMessageHandler<RequestItemsMessage>(this.OnRequestItemsMessage);
        }

        private void OnRequestItemsMessage(MessageEventArgs<RequestItemsMessage> obj)
        {
            switch (obj.Message.RequestedItem)
            {
                case RequestItems.ListMonitors:
                    this.SendMonitorRequestListAsync(obj.Connection);
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
    }
}