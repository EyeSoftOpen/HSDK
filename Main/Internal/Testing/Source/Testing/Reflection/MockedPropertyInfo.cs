namespace EyeSoft.Testing.Reflection
{
	using System.Collections.Generic;
	using System.Reflection;

	public abstract class MockedPropertyInfo
		: PropertyInfo
	{
		public IEnumerable<MockedPropertyInfo> Property<T>(string name)
		{
			yield return this;

			yield return Mocking.Property<T>(name);
		}
	}
}