// <copyright file="CaptureImage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.ScreenRecorder
{
    public class CaptureImage
    {
        public CaptureImage(string screenName, byte[] image, System.Drawing.Rectangle bounds, bool scaled, double scale)
        {
            ScreenName = screenName;
            Image = image;
            Bounds = bounds;
            Scaled = scaled;
            Scale = scale;
        }

        public readonly byte[] Image;
        public readonly string ScreenName;
        public readonly System.Drawing.Rectangle Bounds;
        public readonly bool Scaled;
        public readonly double Scale;
    }
}
