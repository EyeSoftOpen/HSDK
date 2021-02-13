namespace EyeSoft.Core.Test.Net.Mail
{
    using EyeSoft.Net.Mail;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
    public class MailAddressTest
    {
        [TestMethod]
        public void CheckValidMail()
        {
            MailAddress.IsValid("test@domain.com").Should().Be.True();
        }

        [TestMethod]
        public void CheckNotValidMail()
        {
            MailAddress.IsValid("test.domain.com").Should().Be.False();
        }
    }
}