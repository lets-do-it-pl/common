using System;
using FluentAssertions;
using Xunit;

namespace LetsDoIt.CustomValueTypes.Tests
{
    using Image;

    public class ImageTests
    {
        private const string ValidBase64String = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==";

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bad")]
        [InlineData("bad.bad")]
        public void Image_WhenIsNullOrWhiteSpaceOrImageIsInvalid_ShouldThrowArgumentException(string image)
        {
            Action test = () => new Image(image);

            test.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Email_WhenValidValueGiven_ShouldBeEqualToItsStringVersion()
        {
            string imageValue = ValidBase64String;

            Image image = imageValue;

            image.Should().Be(imageValue);
        }
    }
}