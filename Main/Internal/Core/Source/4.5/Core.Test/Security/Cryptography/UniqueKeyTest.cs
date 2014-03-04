namespace EyeSoft.Test.Security.Cryptography
{
	using System;
	using System.Linq;

	using EyeSoft.Security.Cryptography;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

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
				.Should().Be.True();
		}

		[TestMethod]
		public void FromRngOnlyUpperCharacterOrDigit()
		{
			UniqueKey
				.FromRng(CharSet.UpperCase, CharSet.Numbers)
				.All(c => char.IsUpper(c) || char.IsNumber(c))
				.Should().Be.True();
		}

		[TestMethod]
		public void FromRngOnlySimplifiedLowerCaseCharactersAndReducedLength()
		{
			const int CodeLength = 5;

			var code = UniqueKey.FromRng(CodeLength, CharSet.SimplifiedLowerCase);

			code.All(c => CharSet.SimplifiedLowerCase.Contains(c)).Should().Be.True();
			code.Length.Should().Be.EqualTo(CodeLength);
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
			Executing
				.This(() => UniqueKey.FromRandomNumberGeneratedComplex(3))
				.Should().Throw<ArgumentException>();
		}

		private void CheckRandomGenerated(string stringFromRandomNumberGenerator, int expectedLenght)
		{
			var asEnumerable = stringFromRandomNumberGenerator.AsEnumerable().ToArray();

			asEnumerable
				.All(c => char.IsUpper(c) || char.IsLower(c) || char.IsNumber(c) || char.IsPunctuation(c))
				.Should().Be.True();

			asEnumerable.Any(char.IsUpper).Should("The RNG does not contains uppercase.").Be.True();
			asEnumerable.Any(char.IsLower).Should("The RNG does not contains lowercase.").Be.True();
			asEnumerable.Any(char.IsNumber).Should("The RNG does not contains number.").Be.True();
			asEnumerable.Any(char.IsPunctuation).Should("The RNG does not contains symbol.").Be.True();

			stringFromRandomNumberGenerator.Length.Should().Be.EqualTo(expectedLenght);
		}

		private void CheckUniqueKeyMethodIsOnlyLetterOrDigit(UniqueKeyMethod uniqueKeyMethod)
		{
			UniqueKey
				.From(uniqueKeyMethod)
				.All(char.IsLetterOrDigit)
				.Should().Be.True();
		}
	}
}