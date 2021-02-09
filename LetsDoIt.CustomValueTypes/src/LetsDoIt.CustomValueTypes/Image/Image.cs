using System;
using System.Net.Mail;

namespace LetsDoIt.CustomValueTypes.Image
{
    public readonly struct Image
    {
        private readonly string _value;

        public Image(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Invalid image.", value);
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
                throw new ArgumentException("Email can not be empty!");
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
    }
}