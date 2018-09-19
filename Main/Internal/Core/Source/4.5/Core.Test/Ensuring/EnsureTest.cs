namespace EyeSoft.Test.Ensuring
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	using EyeSoft.IO;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class EnsureTest
	{
		[TestMethod]
		public void VerifyNullObjectIsNull()
		{
			object obj = null;

			Ensure
				// ReSharper disable once ExpressionIsAlwaysNull
				.That(obj)
				.Is.Null();
		}

		[TestMethod]
		public void VerifyNullObjectIsNotNull()
		{
			Action<object> action = obj =>
			Ensure
				.That(obj)
				.Is.Not.Null();

			Executing.This(() => action(null)).Should().Throw<NullReferenceException>();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectNotNullException))]
		public void VerifyInstanceIsNull()
		{
			Ensure
				.That(new object())
				.Is.Null();
		}

		[TestMethod]
		public void VerifyInstanceIsNotNull()
		{
			Ensure
				.That(new object())
				.Is.Not.Null();
		}

		[TestMethod]
		public void VerifyEmptyStringThrowException()
		{
			Action ensure = () =>
					Ensure
						.That(string.Empty)
						.Is.Not.NullOrWhiteSpace();
			this
				.Executing(a => ensure())
				.Throws<NullReferenceException>();
		}

		[TestMethod]
		public void VerifyNullParameterValidationThrowCorrectCustomException()
		{
			const string Parameter = "Custom parameter.";

			Action<object> ensure = obj =>
				Ensure
					.That(obj)
					.WithException<CustomException>(e => e.Title = Parameter)
					.Is.Not.Null();

			Executing
				.This(() => ensure(null))
				.Should().Throw<CustomException>()
				.And.Exception.Title
				.Should("The property of the custom exception is not correct.").Be.EqualTo(Parameter);
		}

		[TestMethod]
		public void VerifyDictionaryNotContainsAnElementThrowCorrectExceptionAndCheckParameterNameIsCorrect()
		{
			const string FileName = "file1.tx";

			IDictionary<string, IFileInfo> fileDictionary =
				new Dictionary<string, IFileInfo>();

			Action ensure = () =>
				Ensure.That(fileDictionary)
					.WithException(new FileNotFoundException(string.Format("The file \"{0}\" does not exist.", FileName), FileName))
					.Is.Containing(FileName);

			this
				.Executing(a => ensure())
				.Throws<FileNotFoundException>()
				.And.Exception.FileName.Should("The file name in the FileNotFoundException is not correct.")
				.Be.EqualTo(FileName);
		}
	}
}