using System;
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
            void Test() => new Image(image);

            Assert.ThrowsAny<ArgumentException>(Test);
        }


    }
}