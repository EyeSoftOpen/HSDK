namespace EyeSoft.Core.Test.Security.Cryptography
{
    using System;
    using System.Linq;
    using EyeSoft.Security.Cryptography;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;
    using Text;

    [TestClass]
	public class UniqueKeyTest
	{
		[TestMethod]
		public void FromTicksTestCheckOnlyLetterOrDigit()
		{
			CheckUniqueKeyMethodIsOnlyLetterOrDigit(UniqueKeyMethod.Ticks);
		}

		[TestMethod]
		public void FromDateTime()
		{
			CheckUniqueKeyMethodIsOnlyLetterOrDigit(UniqueKeyMethod.DateTime);
		}

		[TestMethod]
		public void FromGuid()
		{
			CheckUniqueKeyMethodIsOnlyLetterOrDigit(UniqueKeyMethod.Guid);
		}

		[TestMethod]
		public void FromRng()
		{
			CheckUniqueKeyMethodIsOnlyLetterOrDigit(UniqueKeyMethod.RngCharacterMask);
		}

		[TestMethod]
		public void FromRngOnlyUpperCharacter()
		{
			UniqueKey
				.FromRng(CharSet.UpperCase)
				.All(char.IsUpper)
				.Should().BeTrue();
		}

		[TestMethod]
		public void FromRngOnlyUpperCharacterOrDigit()
		{
			UniqueKey
				.FromRng(CharSet.UpperCase, CharSet.Numbers)
				.All(c => char.IsUpper(c) || char.IsNumber(c))
				.Should().BeTrue();
		}

		[TestMethod]
		public void FromRngOnlySimplifiedLowerCaseCharactersAndReducedLength()
		{
			const int CodeLength = 5;

			var code = UniqueKey.FromRng(CodeLength, CharSet.SimplifiedLowerCase);

			code.All(c => CharSet.SimplifiedLowerCase.Contains(c)).Should().BeTrue();
			code.Length.Should().Be(CodeLength);
		}

		[TestMethod]
		public void FromRngCustomLenght()
		{
			const int Lenght = 10;
			CheckRandomGenerated(UniqueKey.FromRng(Lenght), Lenght);
		}

		[TestMethod]
		public void FromRngFixedLenght()
		{
			CheckRandomGenerated(UniqueKey.FromRandomNumberGeneratedComplex(), 12);
		}

		[TestMethod]
		public void FromRngWithLenghtSmallerThanCharsetThrowsException()
		{
			Action action = () => UniqueKey.FromRandomNumberGeneratedComplex(3);

            action.Should().Throw<ArgumentException>();
		}

		private void CheckRandomGenerated(string stringFromRandomNumberGenerator, int expectedLength)
		{
			var asEnumerable = stringFromRandomNumberGenerator.AsEnumerable().ToArray();

			asEnumerable
				.All(c => char.IsUpper(c) || char.IsLower(c) || char.IsNumber(c) || char.IsPunctuation(c))
				.Should().BeTrue();

			asEnumerable.Any(char.IsUpper).Should().BeTrue("The RNG does not contains uppercase.");
			asEnumerable.Any(char.IsLower).Should().BeTrue("The RNG does not contains lowercase.");
			asEnumerable.Any(char.IsNumber).Should().BeTrue("The RNG does not contains number.");
			asEnumerable.Any(char.IsPunctuation).Should().BeTrue("The RNG does not contains symbol.");

			stringFromRandomNumberGenerator.Length.Should().Be(expectedLength);
		}

		private void CheckUniqueKeyMethodIsOnlyLetterOrDigit(UniqueKeyMethod uniqueKeyMethod)
		{
			UniqueKey
				.From(uniqueKeyMethod)
				.All(char.IsLetterOrDigit)
				.Should().BeTrue();
		}
	}
}