// <copyright file="ICapturedFrame.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Drastic.ScreenRecorder
{
    public interface ICapturedFrame
    {
        int Width { get; }

        int Height { get; }

        byte[] Raw { get; }

        CapturedFrameType Type { get; }
    }

    public enum CapturedFrameType
    {
        Unknown,
        TIFF,
        BMP,
    }
}