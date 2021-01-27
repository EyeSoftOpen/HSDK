namespace EyeSoft.Core.Test.Mapping
{
    using System.Linq;
    using Core.Mapping;
    using Core.Mapping.Conventions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

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
				.Should().Have.SameValuesAs("Name", "Data");

			mappedType.Version
				.Name.Should().Be.EqualTo("Version");
		}

		private abstract class VersionedEntityWithBlob
		{
			public string Name { get; protected set; }

			public byte[] Data { get; protected set; }

			public byte[] Version { get; private set; }
		}
	}
}