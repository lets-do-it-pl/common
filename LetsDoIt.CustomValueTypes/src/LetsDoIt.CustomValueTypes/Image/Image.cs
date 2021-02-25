using System;
using System.Text.Json.Serialization;

namespace LetsDoIt.CustomValueTypes.Image
{

    [JsonConverter(typeof(ImageJsonConverter))]
    public readonly struct Image
    {
        private readonly string _value;

        private readonly string _customSizeImage;

        private readonly byte[] _convertedImage;

        private const long DefaultMaxFileSizeAsBytes = 2 * 1000 * 1000;

        public string SmallImage => ImageHelper.Resize(_convertedImage, 256, 256);

        public string MediumImage => ImageHelper.Resize(_convertedImage, 512, 512);

        public string LargeImage => ImageHelper.Resize(_convertedImage, 1080, 1080);

        public string CustomSizeImage => _customSizeImage;

        public string OriginalValue => _value;

        public Image(string value, long maxFileSizeAsBytes = DefaultMaxFileSizeAsBytes) : this()
        {
            if (!IsValid(value, maxFileSizeAsBytes))
            {
                throw new ArgumentException("Invalid image. Too big or wrong format.", value);
            }

            _convertedImage = Convert.FromBase64String(value);

            _value = value;
        } 

        public Image(string value, int width, int height, long maxFileSizeAsBytes = DefaultMaxFileSizeAsBytes) : this()
        {
            if (!IsValid(value, maxFileSizeAsBytes))
            {
                throw new ArgumentException("Invalid image. Too big or wrong format.", value);
            }

            _value = value;

            _convertedImage = Convert.FromBase64String(value);

            _customSizeImage = ImageHelper.Resize(_convertedImage, width, height);
        }

        public static bool TryParse(string candidate, out Image image)
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
                return string.Equals(this._value, objImage._value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        private bool IsValid(string image, long maxFileSizeAsBytes)
        {
            if (string.IsNullOrEmpty(image))
            {
                return false;
            }

            try
            {
                var convertedImage = Convert.FromBase64String(image);

                if (convertedImage.LongLength > DefaultMaxFileSizeAsBytes)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}