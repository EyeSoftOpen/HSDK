namespace EyeSoft.Core.Test.Mapping
{
    using System;
    using System.Linq;
    using EyeSoft.Mapping;
    using EyeSoft.Mapping.Conventions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class TypeMapperTestVersionedEntity
	{
		[TestMethod]
		public void CheckMappingOfVersionedEntity()
		{
			var mappedType =
				TypeMapperFactory
					.CreateByConventions(null, null, new VersionConvention())
					.Map<VersionedEntity>();

			mappedType.Primitives.Single()
				.Name.Should().Be("Name");

			mappedType.Version
				.Name.Should().Be("Version");
		}

		private abstract class VersionedEntity
		{
			public string Name { get; protected set; }

			public DateTime Version { get; private set; }
		}
	}
}