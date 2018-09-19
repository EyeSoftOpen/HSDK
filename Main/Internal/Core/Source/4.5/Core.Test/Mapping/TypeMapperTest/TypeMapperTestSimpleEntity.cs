namespace EyeSoft.Test.Mapping
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Mapping;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeMapperTestSimpleEntity
	{
		[TestMethod]
		public void CheckMappedOfSimpleEntity()
		{
			var mappedType = TypeMapperFactory.Create().Map<Person>();

			mappedType.Primitives.Select(x => x.Name)
				.Should().Have.SameSequenceAs(new[] { "Name", "Age" });

			mappedType.Collections.Single()
				.Name.Should().Be.EqualTo("Addresses");

			mappedType.References.Single()
				.Name.Should().Be.EqualTo("Parent");
		}

		private abstract class Person
		{
			public string Name { get; set; }

			public decimal? Age { get; set; }

			public Person Parent { get; set; }

			public IEnumerable<Address> Addresses { get; protected set; }
		}

		private abstract class Address
		{
			public string Street { get; set; }
		}
	}
}