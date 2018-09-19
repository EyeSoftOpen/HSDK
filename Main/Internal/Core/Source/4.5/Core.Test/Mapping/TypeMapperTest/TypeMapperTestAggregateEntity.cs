namespace EyeSoft.Test.Mapping
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Mapping;

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
					.Map<MappingTester>();

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

		// ReSharper disable once ClassNeverInstantiated.Local
		private class MappingTester
		{
			// ReSharper disable once UnusedMember.Local
			public string Name { get; set; }

			// ReSharper disable once UnusedMember.Local
			public string FullName { get; set; }

			// ReSharper disable once UnusedMember.Local
			public IEnumerable<Order> Orders { get; set; }

			public HeadOffice HeadOffice { get; set; }
		}

		// ReSharper disable once ClassNeverInstantiated.Local
		private class Order
		{
		}

		private class HeadOffice
		{
		}
	}
}