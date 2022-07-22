// <copyright file="LibJpegTurboEncoder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using TurboJpegWrapper;

namespace Drastic.ScreenRecorder.LibJpegTurbo
{
    public class LibJpegTurboEncoder : IFrameEncoder
    {
        private TJCompressor compressor;
        private int quality;

        public LibJpegTurboEncoder(int quality = 100, string? dllPath = null, Action<string>? logger = null)
        {
            this.quality = quality;
            TJInitializer.Initialize(dllPath, logger);
            this.compressor = new TJCompressor();
        }

        public unsafe byte[] EncodeFrame(ICapturedFrame frame)
        {
            fixed (byte* srcBufPtr = frame.ImageData)
            {
                return this.compressor.Compress((IntPtr)srcBufPtr, 4, frame.Width, frame.Height, TJPixelFormats.TJPF_ARGB, TJSubsamplingOptions.TJSAMP_420, this.quality, TJFlags.NONE);
            }
        }
    }
}
