using System;
using Xunit;

namespace LetsDoIt.CustomValueTypes.Tests
{
    using Email;

    public class EmailTests
    {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Email_WhenIsNullOrWhiteSpace_ShouldThrowArgumentException(string email)
        {
           void Test() => new Email(email); 

           Assert.ThrowsAny<ArgumentException>(Test);
        } 
        
        //[Fact]
        //public void Email_WhenPropsAccessedWithoutNew_ShouldThrowArgumentException( )
        //{

        //    Email email = "asd@ada.com";

        //    Assert.Equal("",email.ToString());
        //}
    }
}
