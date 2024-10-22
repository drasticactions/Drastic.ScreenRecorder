﻿// <copyright file="IFrameEncoder.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.ScreenRecorder
{
    public interface IFrameEncoder
    {
        public byte[] EncodeFrame(ICapturedFrame frame);
    }
}
