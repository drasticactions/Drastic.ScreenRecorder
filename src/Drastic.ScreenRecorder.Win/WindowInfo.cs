using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drastic.ScreenRecorder.Win
{
    public class WindowInfo : IWindow
    {
        public WindowInfo(Process process)
        {
            if (!WindowEnumeration.IsWindowValidForCapture(process.MainWindowHandle))
            {
                throw new ArgumentException(nameof(process));
            }

            this.Process = process;
            this.Title = process.MainWindowTitle;
        }

        /// <summary>
        /// Gets the process.
        /// </summary>
        public Process Process { get; }

        /// <inheritdoc/>
        public string Title { get; }

        /// <inheritdoc/>
        public object RawHandler => this.Process.MainWindowHandle;
    }
}
