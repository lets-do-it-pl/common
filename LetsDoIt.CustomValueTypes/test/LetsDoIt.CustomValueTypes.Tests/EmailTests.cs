using System;
using FluentAssertions;
using Xunit;

namespace LetsDoIt.CustomValueTypes.Tests
{
    using Email;

    public class EmailTests
    {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bad")]
        [InlineData("bad.bad")]
        public void Email_WhenIsNullOrWhiteSpaceOrEmailIsInvalid_ShouldThrowArgumentException(string email)
        {
           void Test() => new Email(email); 
           
           Assert.ThrowsAny<ArgumentException>(Test);
        }

        [Fact]
        public void Email_WhenValidValueGiven_ShouldBeEqualToItsStringVersion()
        {
            string emailValue = "good@good.good";

            Email email = emailValue;

            email.Should().Be(emailValue);
        }
        
        [Fact]
        public void Email_ShouldBeCaseInsensitive()
        {
            string emailValue = "GOOD@GOOD.GOOD";

            Email email = emailValue;

            Assert.Equal(emailValue.ToLower(), email.ToString());
        } 
        
        [Fact]
        public void EmailEquals_ShouldCompareValuesAndShouldBeCaseInsensitive()
        {
            string emailValue = "GOOD@GOOD.GOOD";

            Email email = emailValue;

            Assert.True(email.Equals(email));
        } 

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bad")]
        [InlineData("bad.bad")]
        public void EmailParse_WhenIsNullOrWhiteSpaceOrEmailIsInvalid_ShouldThrowArgumentException(string email)
        {
            void Test() => Email.Parse(email);

            Assert.ThrowsAny<ArgumentException>(Test);
        }   
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bad")]
        [InlineData("bad.bad")]
        public void EmailTryParse_WhenIsNullOrWhiteSpaceOrEmailIsInvalid_ShouldReturnFalse(string canditate)
        {
            var result = Email.TryParse(canditate, out Email email);

            Assert.False(result);
        } 
    }
}
