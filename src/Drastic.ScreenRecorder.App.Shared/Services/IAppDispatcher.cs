// <copyright file="IAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.ScreenRecorder
{
    /// <summary>
    /// App Dispatcher.
    /// Sends events for the given UI thread.
    /// </summary>
    public interface IAppDispatcher
    {
        /// <summary>
        /// Dispatches action.
        /// </summary>
        /// <param name="action">Action to dispatch.</param>
        /// <returns>Bool if dispatch occured.</returns>
        bool Dispatch(Action action);
    }
}