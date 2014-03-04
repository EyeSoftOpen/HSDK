namespace EyeSoft.Test.Mapping.Strategies
{
	using EyeSoft.Mapping;
	using EyeSoft.Mapping.Strategies;
	using EyeSoft.Testing.Reflection;

	using SharpTestsEx;

	public abstract class PropertyStrategyTest<T>
		where T : IMemberStrategy, new()
	{
		protected void CheckProperty<TProperty>(bool expected, params object[] attributes)
		{
			new T()
				.HasToMap(new PrimitiveMemberInfoMetadata(Mocking.Property<TProperty>("PropertyName", attributes)))
				.Should().Be.EqualTo(expected);
		}
	}
}