using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;

namespace Drastic.ScreenRecorder.Win
{
    public class CapturedFrame : ICapturedFrame
    {
        public CapturedFrame(SoftwareBitmap bitmap)
        {
            this.Bitmap = bitmap;
            this.Width = bitmap.PixelWidth;
            this.Height = bitmap.PixelWidth;
        }

        public int Width { get; }

        public int Height { get; }

        public object Raw => this.Bitmap;

        public SoftwareBitmap Bitmap { get; }

    }
}
