namespace EyeSoft.Core.Test.Net.Mail
{
    using EyeSoft.Net.Mail;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
    public class MailAddressTest
    {
        [TestMethod]
        public void CheckValidMail()
        {
            MailAddress.IsValid("test@domain.com").Should().BeTrue();
        }

        [TestMethod]
        public void CheckNotValidMail()
        {
            MailAddress.IsValid("test.domain.com").Should().BeFalse();
        }
    }
}