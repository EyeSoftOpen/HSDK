namespace EyeSoft.Core.Test.Mapping
{
    using System.Linq;
    using EyeSoft.Mapping;
    using EyeSoft.Mapping.Conventions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class TypeMapperTestVersionedEntityAndBlob
	{
		[TestMethod]
		public void CheckMappingOfVersionedEntity()
		{
			var mappedType =
				TypeMapperFactory
					.CreateByConventions(null, null, new VersionConvention())
					.Map<VersionedEntityWithBlob>();

			mappedType
				.Primitives
				.Select(x => x.Name)
				.Should().BeEquivalentTo("Name", "Data");

			mappedType.Version
				.Name.Should().Be("Version");
		}

		private abstract class VersionedEntityWithBlob
		{
			public string Name { get; protected set; }

			public byte[] Data { get; protected set; }

			public byte[] Version { get; private set; }
		}
	}
}