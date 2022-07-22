// <copyright file="RemoteClient.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public sealed class RemoteClient
        : TempestClient
    {
        public RemoteClient(IClientConnection connection)
            : base(connection, MessageTypes.Reliable)
        {
            this.RegisterMessageHandler<ListMonitorsMessage>(this.OnListMonitorsMessage);
        }

        public event EventHandler<MessageEventArgs<ListMonitorsMessage>>? OnRecievedListMonitorsMessage;

        private void OnListMonitorsMessage(MessageEventArgs<ListMonitorsMessage> obj)
            => this.OnRecievedListMonitorsMessage?.Invoke(this, obj);

        public Task<bool> SendMessage(Message chat)
        {
            return Connection.SendAsync(chat);
        }
    }
}