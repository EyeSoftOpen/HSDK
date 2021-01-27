namespace EyeSoft.Core.Test.Reflection
{
    using Core.Reflection;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class PropertyInfoExtensionsTest
	{
		[TestMethod]
		public void CheckIfPropertyIsPrivate()
		{
			CheckScopeIsCorrect(
				"Private",
				"A private property is not recognized as private.",
				true,
				"A private property is recognized as public.",
				false);
		}

		[TestMethod]
		public void CheckIfPropertyIsNotPrivate()
		{
			CheckScopeIsCorrect(
				"Public",
				"A public property is recognized as private.",
				false,
				"A public property is not recognized as public.",
				true);
		}

		[TestMethod]
		public void VerifyMemberExpressionFromPropertyInfoIsCorrect()
		{
			new TypeReflector<PrivatePropertyClass>()
				.PropertyExpression(x => x.Public)
				.MemberExpression<PrivatePropertyClass, string>()
				.ToString().Should().Be.EqualTo("x => x.Public");
		}

		private static void CheckScopeIsCorrect(
			string propertyName,
			string privatePropertyMessage,
			bool privateExpected,
			string publicPropertyMessage,
			bool publicExpected)
		{
			var property =
				typeof(PrivatePropertyClass)
					.GetAnyInstanceProperty(propertyName);

			property.IsPrivate().Should(privatePropertyMessage).Be.EqualTo(privateExpected);
			property.IsPublic().Should(publicPropertyMessage).Be.EqualTo(publicExpected);
		}

		private class PrivatePropertyClass
		{
			public string Public { get; set; }

			private string Private { get; set; }
		}
	}
}