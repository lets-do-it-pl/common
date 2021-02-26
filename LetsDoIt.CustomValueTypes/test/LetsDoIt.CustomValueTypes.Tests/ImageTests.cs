using System;
using FluentAssertions;
using Xunit;

namespace LetsDoIt.CustomValueTypes.Tests
{
    using Image;

    public class ImageTests
    {
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


    }
}