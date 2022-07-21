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
            this.Raw = this.ConvertToByteArray(bitmap);
            this.Width = bitmap.PixelWidth;
            this.Height = bitmap.PixelWidth;
            this.Type = CapturedFrameType.BMP;
        }

        public int Width { get; }

        public int Height { get; }

        public byte[] Raw { get; }

        public SoftwareBitmap Bitmap { get; }

        public CapturedFrameType Type { get; }

        private byte[] ConvertToByteArray(SoftwareBitmap bitmap)
        {
            byte[] imageBytes = new byte[4 * bitmap.PixelWidth * bitmap.PixelHeight];
            bitmap.CopyToBuffer(imageBytes.AsBuffer());
            return imageBytes;
        }
    }
}
