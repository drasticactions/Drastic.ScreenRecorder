using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drastic.ScreenRecorder.LibJpegTurbo.Tests
{
    [TestClass]
    public class ImageTests
    {
        [DataRow(@"images/nightshot_iso_100.bmp", 192, 144)]
        [DataTestMethod]
        public async Task ImageCompressTest(string filepath, int width, int height)
        {

            Directory.CreateDirectory("output");

            var compressor = new LibJpegTurboEncoder();

            var bytes = File.ReadAllBytes(filepath);

            var testResult = compressor.EncodeFrame(new MockCapturedFrame(width, height, bytes));

            File.WriteAllBytes($"output/{Guid.NewGuid()}.jpg", bytes);
        }
    }

    public class MockCapturedFrame : ICapturedFrame
    {
        public MockCapturedFrame(int width, int height, byte[] data)
        {
            this.Width = width;
            this.Height = height;
            this.ImageData = data;
        }

        public int Width { get; }

        public int Height { get; }

        public byte[] ImageData { get; }

        public object Raw => this.ImageData;
    }
}
