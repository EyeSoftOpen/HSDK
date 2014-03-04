namespace EyeSoft.Test.Mapping
{
	using System;
	using System.Linq;

	using EyeSoft.Mapping;
	using EyeSoft.Mapping.Conventions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

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
				.Name.Should().Be.EqualTo("Name");

			mappedType.Version
				.Name.Should().Be.EqualTo("Version");
		}

		private abstract class VersionedEntity
		{
			public string Name { get; protected set; }

			public DateTime Version { get; private set; }
		}
	}
}