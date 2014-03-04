namespace EyeSoft.Test.Mapping
{
	using System.Linq;

	using EyeSoft.Mapping;
	using EyeSoft.Testing.Domain.Helpers.Domain2;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeMapperTestAggregateEntity
	{
		[TestMethod]
		public void CheckMappingOfAggregateEntity()
		{
			var mappedType =
				TypeMapperFactory
					.Create()
					.Map<CustomerAggregate>();

			mappedType
				.Primitives.Single(member => member.Name == "Name")
				.Name.Should().Be.EqualTo("Name");

			mappedType
				.Primitives.Single(member => member.Name == "FullName")
				.Name.Should().Be.EqualTo("FullName");

			mappedType
				.Collections.Single()
				.Name.Should().Be.EqualTo("Orders");

			mappedType
				.References.Single()
				.Name.Should().Be.EqualTo("HeadOffice");
		}
	}
}