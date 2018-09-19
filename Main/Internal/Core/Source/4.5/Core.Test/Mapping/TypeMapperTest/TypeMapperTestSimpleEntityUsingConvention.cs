namespace EyeSoft.Test.Mapping
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	using EyeSoft.Mapping;
	using EyeSoft.Reflection;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeMapperTestSimpleEntityUsingConvention
	{
		[TestMethod]
		public void CheckMappedOfSimpleEntityWithMetadaAttribute()
		{
			var mappedType =
				TypeMapperFactory.CreateByConventions().Map<PersonWithMetadata>();

			mappedType.Source.Should().Be.EqualTo(typeof(PersonWithMetadata));
			mappedType.Mapped.Should().Be.EqualTo(typeof(PersonMetadata));

			var primitiveProperty =
				mappedType.Primitives.Single();

			var reflector = new TypeReflector<PersonWithMetadata>();

			primitiveProperty.Name.Should().Be.EqualTo(reflector.PropertyName(x => x.Name));

			primitiveProperty.Required.Should("The required attribute was not recognized.").Be.True();

			mappedType.Collections.Single()
				.Name.Should().Be.EqualTo(reflector.PropertyName(x => x.Addresses));

			mappedType.References.Single()
				.Name.Should().Be.EqualTo(reflector.PropertyName(x => x.Parent));
		}

		[MetadataType(typeof(PersonMetadata))]
		private abstract class PersonWithMetadata
		{
			public string Name { get; set; }

			public PersonWithMetadata Parent { get; set; }

			public IEnumerable<Address> Addresses { get; protected set; }
		}

		private class PersonMetadata
		{
			[Required]
			public string Name { get; set; }

			public PersonWithMetadata Parent { get; set; }

			public IEnumerable<Address> Addresses { get; protected set; }
		}
	}
}