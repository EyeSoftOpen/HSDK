namespace EyeSoft.Core.Test.Mapping.Strategies
{
    using EyeSoft.Mapping;
    using EyeSoft.Mapping.Strategies;
    using FluentAssertions;

    public abstract class PropertyStrategyTest<T>
		where T : IMemberStrategy, new()
	{
		protected void CheckProperty<TProperty>(bool expected, params object[] attributes)
		{
			new T()
				.HasToMap(new PrimitiveMemberInfoMetadata(Mocking.Property<TProperty>("PropertyName", attributes)))
				.Should().Be(expected);
		}
	}
}