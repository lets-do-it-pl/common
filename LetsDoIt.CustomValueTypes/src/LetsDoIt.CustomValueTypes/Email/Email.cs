using System;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace LetsDoIt.CustomValueTypes.Email
{

    [JsonConverter(typeof(EmailJsonConverter))]
    public readonly struct Email
    {
        private readonly string _value;

        public Email(string value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("Invalid email address.", value);
            }

            _value = value.ToLowerInvariant();
        }

        public static bool TryParse(string candidate, out Email email)
        {
            email = default;

            try
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    return false;
                }

                email = new Email(candidate);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static Email Parse(string candidate)
        {
            return new Email(candidate);
        }

        public static explicit operator string(Email email)
        {
            return email._value;
        }

        public static implicit operator Email(string email)
        {
            return new Email(email);
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                Email objEmail => string.Equals(this._value, objEmail._value,
                    StringComparison.InvariantCultureIgnoreCase),
                string objString => string.Equals(this._value, objString, StringComparison.InvariantCultureIgnoreCase),
                _ => false
            };
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        private static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var mailAddress = new MailAddress(email);

                return string.Equals(mailAddress.Address, email, StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return false;
            }

        }
    }
}