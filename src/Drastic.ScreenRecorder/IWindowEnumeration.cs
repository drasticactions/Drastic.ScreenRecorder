using System;
using System.Collections.Generic;
using System.Text;

namespace Drastic.ScreenRecorder
{
    public interface IWindowEnumeration
    {
        IReadOnlyList<IWindow> GetWindows();

        Task<IReadOnlyList<IWindow>> GetWindowsAsync();
    }
}
