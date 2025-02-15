﻿// <copyright file="RemoteTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ScreenRecorder.LibJpegTurbo;
using Drastic.ScreenRecorder.Win;
using Drastic.Tempest;
using Drastic.Tempest.Providers.Network;

namespace Drastic.ScreenRecorder.Remote.Win.Tests
{
    [TestClass]
    public class RemoteTests
    {
        private RemoteServer server;
        private RemoteClient client;
        private Target target;

        public RemoteTests()
        {

        }

        [TestInitialize]
        public async Task Setup()
        {
            var ips = NetworkUtils.DeviceIps().ToList();
            this.target = new Target(ips.Last(), 8888);
            var provider = new NetworkConnectionProvider(RemoteProtocol.Instance, this.target, 100);
            this.server = new RemoteServer(new LibJpegTurboEncoder(), new CaptureSessionFactory(), new MonitorEnumeration(), new WindowEnumeration(), provider);
            this.server.Start();
            await Task.Delay(1000);
            var connection = new NetworkClientConnection(RemoteProtocol.Instance);
            this.client = new RemoteClient(connection);
            var result = await client.ConnectAsync(this.target);
            if (result is null || result.Result != ConnectionResult.Success)
                Assert.Fail("Can't connect to server.");
        }

        [TestCleanup]
        public async Task TearDown()
        {
            if (this.client is not null)
                await this.client.DisconnectAsync();
            if (this.server is not null)
                this.server.Stop();
        }

        [TestMethod]
        public async Task ListMonitorsMessage()
        {
            var cs = new CancellationTokenSource();
            this.client.OnRecievedListMonitorsMessage += Client_OnRecievedListMonitorsMessage;
            await this.client.SendMessage(new RequestItemsMessage() { RequestedItem = RemoteMessageType.ListMonitors });
            await this.FailAfterTimeout(3000, cs.Token);

            void Client_OnRecievedListMonitorsMessage(object? sender, MessageEventArgs<ListMonitorsMessage> e)
            {
                Assert.IsNotNull(e?.Message);
                Assert.IsTrue(e.Message.Monitors.Any());
                cs.Cancel();
            }
        }

        [TestMethod]
        public async Task CaptureMonitorSession()
        {
            Monitor? monitor = null;
            var cs = new CancellationTokenSource();
            this.client.OnRecievedListMonitorsMessage += Client_OnRecievedListMonitorsMessage;
            this.client.OnRecievedCaptureSessionFrameMessage += Client_OnRecievedCaptureSessionFrameMessage;
            await this.client.SendMessage(new RequestItemsMessage() { RequestedItem = RemoteMessageType.ListMonitors });
            await Task.Delay(10000);
            Assert.IsNotNull(monitor);
            await this.client.SendMessage(new CaptureSessionMessage() { TargetId = monitor.DeviceName, CaptureSessionFunction = CaptureSessionFunction.Start, TargetType = TargetType.Display });

            await Task.Delay(-1);

            void Client_OnRecievedCaptureSessionFrameMessage(object? sender, MessageEventArgs<CaptureSessionFrameMessage> e)
            {
                Assert.IsNotNull(e?.Message);
            }

            void Client_OnRecievedListMonitorsMessage(object? sender, MessageEventArgs<ListMonitorsMessage> e)
            {
                Assert.IsNotNull(e?.Message);
                Assert.IsTrue(e.Message.Monitors.Any());
                monitor = e.Message.Monitors.First();
                cs.Cancel();
            }
        }

        [TestMethod]
        public async Task ListWindowsMessage()
        {
            var cs = new CancellationTokenSource();
            this.client.OnRecievedListWindowsMessage += Client_OnRecievedListWindowsMessage;
            await this.client.SendMessage(new RequestItemsMessage() { RequestedItem = RemoteMessageType.ListWindows });
            await this.FailAfterTimeout(3000, cs.Token);

            void Client_OnRecievedListWindowsMessage(object? sender, MessageEventArgs<ListWindowsMessage> e)
            {
                Assert.IsNotNull(e?.Message);
                Assert.IsTrue(e.Message.Windows.Any());
                cs.Cancel();
            }
        }

        private async Task FailAfterTimeout(int mils, CancellationToken token, string failMessage = "Timeout!")
        {
            try
            {
                await Task.Delay(mils, token);
                Assert.Fail(failMessage);
            }
            catch (TaskCanceledException)
            {
                // We expect this, continue.
            }
        }
    }
}
