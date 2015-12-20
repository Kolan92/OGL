using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ImageResizer;
using System.IO;

namespace Services
{
    public static class ImageOptimization
    {
        public static bool ValidateImage(byte[] image)
        {
            return GetImageExtension(image) != ImageExtension.unknown;
        }

        public static ImageExtension GetImageExtension(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");
            var gif = Encoding.ASCII.GetBytes("GIF");
            var png = new byte[] { 137, 80, 78, 71 };
            var jpeg = new byte[] { 255, 216, 255, 224 };
            var jpegCanon = new byte[] { 255, 216, 255, 255 };

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageExtension.bmp;
            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageExtension.gif;
            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageExtension.png;
            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageExtension.jpeg;
            if (jpegCanon.SequenceEqual(bytes.Take(jpegCanon.Length)))
                return ImageExtension.jpeg;

            return ImageExtension.unknown;
        }

        public static byte[] OptimizeImageFromBytes(int imgWidth, int imgHeight, byte[] imgBytes)
        {
            var settings = new ResizeSettings
            {
                MaxHeight = imgHeight,
                MaxWidth = imgWidth,
            };

            MemoryStream ms = new MemoryStream();
            ImageBuilder.Current.Build(imgBytes, ms, settings);
            return ms.ToArray();
        }
    }
}