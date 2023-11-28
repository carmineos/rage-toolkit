// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TextureTool.Models
{
    // source: http://stackoverflow.com/questions/21428272/show-rgba-image-from-memory
    public class RgbaBitmapSource : BitmapSource
    {
        private byte[] buffer;
        private int width;
        private int height;

        public RgbaBitmapSource(byte[] buffer, int width, int height)
        {
            this.buffer = buffer;
            this.width = width;
            this.height = height;
        }

        public override void CopyPixels(
            Int32Rect sourceRect, Array pixels, int stride, int offset)
        {
            for (int y = sourceRect.Y; y < sourceRect.Y + sourceRect.Height; y++)
            {
                for (int x = sourceRect.X; x < sourceRect.X + sourceRect.Width; x++)
                {
                    int i = stride * y + 4 * x;

                    byte a = buffer[i + 3];
                    byte r = (byte)(buffer[i] * a / 256);
                    byte g = (byte)(buffer[i + 1] * a / 256);
                    byte b = (byte)(buffer[i + 2] * a / 256);

                    pixels.SetValue(b, i + offset);
                    pixels.SetValue(g, i + offset + 1);
                    pixels.SetValue(r, i + offset + 2);
                    pixels.SetValue(a, i + offset + 3);
                }
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new RgbaBitmapSource(buffer, width, height);
        }

        public override event EventHandler<DownloadProgressEventArgs> DownloadProgress;
        public override event EventHandler DownloadCompleted;
        public override event EventHandler<ExceptionEventArgs> DownloadFailed;
        public override event EventHandler<ExceptionEventArgs> DecodeFailed;

        public override double DpiX
        {
            get { return 96; }
        }

        public override double DpiY
        {
            get { return 96; }
        }

        public override PixelFormat Format
        {
            get { return PixelFormats.Pbgra32; }
        }

        public override int PixelWidth
        {
            get { return width; }
        }

        public override int PixelHeight
        {
            get { return height; }
        }

        public override double Width
        {
            get { return width; }
        }

        public override double Height
        {
            get { return height; }
        }
    }
}
