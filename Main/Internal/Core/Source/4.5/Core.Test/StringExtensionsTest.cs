namespace EyeSoft.Core.Test
{
    using System;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
    public class StringExtensionsTest
    {
        private enum EnumTest
        {
            Value1,
            // ReSharper disable once UnusedMember.Local
            Value2
        }

        [TestMethod]
        public void VerifyParsingOfValidEnumIsCorrect()
        {
            "Value1".ToEnum<EnumTest>().Should().Be(EnumTest.Value1);
        }

        [TestMethod]
        public void VerifyParsingOfNotValidThrow()
        {
            Action action = () => "NotValidValue".ToEnum<EnumTest>();
            
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ConvertStringToByteArray()
        {
            "Hello!".ToByteArray()
                .Should().BeSameAs(new byte[] { 72, 0, 101, 0, 108, 0, 108, 0, 111, 0, 33, 0 });
        }

        [TestMethod]
        public void ContainsInvariantLowerCaseShouldBeTrue()
        {
            "HEllo".ContainsInvariant("he").Should().BeTrue();
        }

        [TestMethod]
        public void ContainsInvariantUpperCaseShouldBeTrue()
        {
            "hello".ContainsInvariant("HE").Should().BeTrue();
        }

        [TestMethod]
        public void SplittedToTitleCaseIsCorrect()
        {
            @"c:\test\TWO paRts".SplittedToTitleCase('\\').Should().Be(@"C:\Test\Two Parts");
        }

        [TestMethod]
        public void ContainsUpperCaseShouldBeTrue()
        {
            @"c:\test\TEMP".ContainsUpperCase().Should().BeTrue();
        }

        [TestMethod]
        public void ReplaceOfMultipleStrings()
        {
            "Bill,Tony!Mike".Replace(new[] { ",", "!" }, ";").Should().Be("Bill;Tony;Mike");
        }

        [TestMethod]
        public void ReplaceOfMultipleCharsWithNull()
        {
            "Bill,Tony!Mike".Replace(new[] { ',', '!' }).Should().Be("BillTonyMike");
        }

        [TestMethod]
        public void ReplaceOfMultipleCharsWithNew()
        {
            "Bill,Tony!Mike".Replace(new[] { ',', '!' }, ';').Should().Be("Bill;Tony;Mike");
        }
    }
}