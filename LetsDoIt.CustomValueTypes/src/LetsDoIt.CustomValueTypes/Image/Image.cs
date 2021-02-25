using System;
using System.IO;
using System.Drawing;

namespace LetsDoIt.CustomValueTypes.Image
{
    public readonly struct Image
    {
        private readonly string _value;

        private static byte[] _convertedImage;

        public string SmallImage => ConvertToSmallImage();

        public string MediumImage => throw new NotImplementedException();

        public string LargeImage => throw new NotImplementedException();

        public Image(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Invalid image. Too big or wrong format.", value);
            }
            _value = value.ToLowerInvariant();
        }

        public static bool IsValid(string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                return false;
            }

            try
            {
                var convertedImage = Convert.FromBase64String(image);

                if (convertedImage.LongLength > 1024)
                {
                    return false;
                }

                _convertedImage = convertedImage;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryParse(string candidate, out Image? image)
        {
            image = null;

            if (string.IsNullOrWhiteSpace(candidate))
            {
                return false;
            }

            image = new Image(candidate);

            return true;
        }

        public static Image Parse(string candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                throw new ArgumentException("Image can not be empty!");
            }

            return new Image(candidate);
        }

        public static explicit operator string(Image image)
        {
            return image._value;
        }

        public static implicit operator Image(string image)
        {
            return new Image(image);
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Image objImage)
            {
                return string.Equals(this._value, objImage._value, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        private string ConvertToSmallImage()
        {

            return "";
        }
    }
}