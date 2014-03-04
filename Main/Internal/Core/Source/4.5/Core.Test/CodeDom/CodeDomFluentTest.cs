namespace EyeSoft.Test.CodeDom
{
	using EyeSoft.CodeDom;
	using EyeSoft.CodeDom.Converters;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class CodeDomFluentTest
	{
		[TestMethod]
		public void VerifyGeneratedCodeVoidMethod()
		{
			var codeDomFluent =
				new CodeDomFluent()
					.Namespace("EyeSoft.Sample")
						.Class("Test")
							.Using("System")
							.AddMethod("Print")
								.AddParameter<string>("line")
								.Body("Console.WriteLine(line)");

			var code =
				new CodeDomFluentConverter()
					.ConvertToString(codeDomFluent);

			code
				.Should()
				.Be
				.EqualTo(KnownCode.VoidMethod);
		}

		[TestMethod]
		public void VerifyGeneratedCodeMethodWithReturnType()
		{
			var codeDomFluent =
				new CodeDomFluent()
					.Namespace("EyeSoft.Sample")
						.Class("Test")
							.Using("System")
							.AddMethod<string>("Read")
								.AddParameter<string>("line")
								.Body()
									.AddLine("Console.WriteLine(line)")
									.Return("Console.ReadLine()");

			var code =
				new CodeDomFluentConverter()
					.ConvertToString(codeDomFluent);

			code
				.Should()
				.Be
				.EqualTo(KnownCode.ReturnTypeMethod);
		}
	}
}