namespace EyeSoft.Testing.Reflection
{
	using System;
	using System.Linq;
	using System.Reflection;

	using Moq;

	public static class Mocking
	{
		public static MockedPropertyInfo Property<T>(string name, params object[] attributes)
		{
			return Property<T>(Visibility.Public, name, attributes);
		}

		public static MockedPropertyInfo PrivateProperty<T>(string name, params object[] attributes)
		{
			return
				Property<T>(Visibility.Public, name, attributes);
		}

		private static MockedPropertyInfo Property<T>(Visibility visibility, string name, params object[] attributes)
		{
			var mock = new Mock<MockedPropertyInfo>();

			mock
				.SetupGet(p => p.Name)
				.Returns(name);

			mock
				.SetupGet(p => p.PropertyType)
				.Returns(typeof(T));

			mock
				.Setup(p => p.GetCustomAttributes(It.IsAny<Type>(), It.IsAny<bool>()))
				.Returns((Type type, bool inherit) => attributes.Where(attribute => attribute.GetType() == type).ToArray());

			mock
				.SetupGet(p => p.MemberType)
				.Returns(MemberTypes.Property);

			return
				mock.Object;
		}
	}
}