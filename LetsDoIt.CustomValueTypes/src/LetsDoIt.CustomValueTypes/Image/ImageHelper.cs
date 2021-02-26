using System;
using System.Drawing;
using System.IO;

namespace LetsDoIt.CustomValueTypes.Image
{
    public static class ImageHelper
    {
        public static string Resize(this byte[] image, int width, int height)
        {
            using var ms = new MemoryStream(image);
            var convertedImage = System.Drawing.Image.FromStream(ms);

            var ratioX = (double)width / convertedImage.Width;
            var ratioY = (double)height / convertedImage.Height;

            var ratio = Math.Min(ratioX, ratioY);

            var finalWidth = (int)(convertedImage.Width * ratio);
            var finalHeight = (int)(convertedImage.Height * ratio);

            var bmp = new Bitmap(finalWidth, finalHeight);
            var graphic = Graphics.FromImage(bmp);
            graphic.DrawImage(convertedImage, 0, 0, finalWidth, finalHeight);

            using var mStream = new MemoryStream();
            bmp.Save(mStream, bmp.RawFormat);

            var finale= mStream.ToArray();

            return Convert.ToBase64String(finale);
        }
    }
}
