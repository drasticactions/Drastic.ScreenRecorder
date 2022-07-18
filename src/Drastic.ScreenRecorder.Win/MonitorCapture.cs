using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drastic.ScreenRecorder.Win
{
    public class MonitorCapture : IMonitorCaptureSurface
    {
        public MonitorCapture(MonitorInfo info)
        {
            this.Monitor = info;
            this.Surface = new MonitorCaptureItem(info);
        }

        public IMonitor Monitor { get; }

        public ICaptureSurface Surface { get; }
    }
}
