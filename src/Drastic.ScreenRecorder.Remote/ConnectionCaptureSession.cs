// <copyright file="ConnectionCaptureSession.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tempest;

namespace Drastic.ScreenRecorder.Remote
{
    public class ConnectionCaptureSession
    {
        private IConnection connection;
        private ICaptureSession session;
        private IFrameEncoder encoder;

        public ConnectionCaptureSession(IConnection connection, IFrameEncoder encoder, ICaptureSession session)
        {
            this.connection = connection;
            this.session = session;
            this.encoder = encoder;
        }

        public IConnection Connection => this.connection;

        public ICaptureSession Session => this.session;


        public async Task Start()
        {
            this.session.OnCapturedFrame += Session_OnCapturedFrame;
            this.session.Start();

        }

        public async Task Stop()
        {
            this.session.OnCapturedFrame -= Session_OnCapturedFrame;
            this.session.Stop();
        }

        private async void Session_OnCapturedFrame(object? sender, CapturedFrameEventArgs e)
        {
            var frame = this.encoder.EncodeFrame(e.Frame);
            var result = await this.Connection.SendAsync(new CaptureSessionFrameMessage() { TargetId = "test", ImageData = frame });
        }
    }
}
