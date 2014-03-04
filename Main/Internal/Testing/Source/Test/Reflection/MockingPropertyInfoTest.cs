namespace EyeSoft.Testing.Test.Reflection
{
	using EyeSoft.Testing.Reflection;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class MockingPropertyInfoTest
	{
		private const string Name = "Name";

		[TestMethod]
		public void CheckPropertyNameAndTypeIsCorrect()
		{
			var property =
				Mocking
					.Property<string>(Name);

			property
				.Name.Should("The property name is wrong.").Be.EqualTo(Name);

			property
				.PropertyType.Should("The property type is wrong.").Be.EqualTo(typeof(string));
		}
	}
}