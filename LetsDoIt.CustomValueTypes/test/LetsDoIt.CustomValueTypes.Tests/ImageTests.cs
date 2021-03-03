using System;
using System.IO;
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
        public void Image_WhenValidValueGiven_ShouldBeEqualToItsStringVersion()
        {
            Image image = ValidBase64String;

            image.Should().Be(ValidBase64String);
        }

        [Fact]
        public void ImageEquals_ShouldCompareValues()
        {
            Image image = ValidBase64String;

            image.Equals(ValidBase64String).Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bad")]
        [InlineData("bad.bad")]
        public void EmailParse_WhenIsNullOrWhiteSpaceOrImageIsInvalid_ShouldThrowArgumentException(string image)
        {
            Action test = () => Image.Parse(image);

            test.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bad")]
        [InlineData("bad.bad")]
        public void ImageTryParse_WhenIsNullOrWhiteSpaceOrImageIsInvalid_ShouldReturnFalse(string candidate)
        {
            Image.TryParse(candidate, out _).Should().BeFalse();
        }

        [Fact]
        public void Image_WhenImageSizeIsBiggerThanMaxFileSize_ShouldThrowArgumentException()
        {
            Action test = () => new Image(ValidBase64String, 2);

            test.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Image_WhenCustomImageSizesAreGiven_ShouldResizeAccordingToIt()
        {
            //It is important that for this test to pass dimensions' ratio should be equal to original image.
            var width = 500;
            var height = 500;

            var image = new Image(ValidBase64String, width, height);

            using var ms = new MemoryStream(Convert.FromBase64String(image.CustomSize));
            var convertedImage = System.Drawing.Image.FromStream(ms);

            convertedImage.Width.Should().Be(width);
            convertedImage.Height.Should().Be(height);
        }

        [Fact]
        public void ImageOriginalValueProperty_ShouldReturnItsOriginalValue()
        {
            var image = new Image(ValidBase64String);

            image.OriginalValue.Should().Be(ValidBase64String);
        }

        [Fact]
        public void ImageToString_ShouldReturnItsOriginalValue()
        {
            var image = new Image(ValidBase64String);

            image.ToString().Should().Be(ValidBase64String);
        }
    }
}