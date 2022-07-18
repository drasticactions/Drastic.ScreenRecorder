// <copyright file="CapturedFrame.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CoreMedia;
using CoreVideo;

namespace Drastic.ScreenRecorder.Mac
{
    /// <summary>
    /// Captured Frame.
    /// </summary>
    public class CapturedFrame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapturedFrame"/> class.
        /// </summary>
        /// <param name="sample"><see cref="CMSampleBuffer"/>.</param>
        public CapturedFrame(CMSampleBuffer sample)
        {
            this.SampleBuffer = sample;
            var attachments = sample.GetAttachments(CMAttachmentMode.ShouldPropagate);

            using var imageBuffer = this.SampleBuffer.GetImageBuffer() as CVPixelBuffer;
            if (imageBuffer is not null)
            {
                this.Surface = imageBuffer.GetIOSurface();
            }
        }

        public CMSampleBuffer SampleBuffer { get; }

        public IOSurface.IOSurface? Surface { get; }

        public CGRect? ContentRect { get; }

        public double ContentScale { get; }

        public double ScaleFactor { get; }
    }
}
