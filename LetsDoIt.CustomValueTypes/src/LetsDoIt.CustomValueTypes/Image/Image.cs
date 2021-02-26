using System;
using System.Text.Json.Serialization;

namespace LetsDoIt.CustomValueTypes.Image
{

    [JsonConverter(typeof(ImageJsonConverter))]
    public readonly struct Image
    {
        private readonly string _value;

        private readonly byte[] _convertedImage;

        private const long DefaultMaxFileSizeAsBytes = 2 * 1000 * 1000;

        public string SmallSize => _convertedImage.Resize(256, 256);

        public string MediumSize => _convertedImage.Resize(512, 512);

        public string LargeSize => _convertedImage.Resize(1080, 1080);

        public string CustomSize { get; }

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

            CustomSize = _convertedImage.Resize(width, height);
        }

        public static bool TryParse(string candidate, out Image image)
        {
            image = default;

            try
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    return false;
                }

                image = new Image(candidate);
            }
            catch
            {
                return false;
            }

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
            return obj switch
            {
                Image objImage => string.Equals(this._value, objImage._value),
                string objString => string.Equals(this._value, objString),
                _ => false
            };
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

                if (convertedImage.LongLength > maxFileSizeAsBytes)
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