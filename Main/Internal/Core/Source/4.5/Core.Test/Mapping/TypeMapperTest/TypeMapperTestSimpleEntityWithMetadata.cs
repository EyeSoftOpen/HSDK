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
	public class TypeMapperTestSimpleEntityWithMetadata
	{
		[TestMethod]
		public void CheckMappedOfSimpleEntityUsingConvention()
		{
			var mappedType =
				TypeMapperFactory
					.CreateByConventions()
					.Map<PersonWithConvention>();

			mappedType.Source.Should().Be.EqualTo(typeof(PersonWithConvention));
			mappedType.Mapped.Should().Be.EqualTo(typeof(PersonWithConventionMetadata));

			var primitiveProperty =
				mappedType.Primitives.Single();

			primitiveProperty.Name.Should().Be.EqualTo(Reflector.PropertyName<PersonWithConvention>(x => x.Name));
			primitiveProperty.Required.Should("The required attribute was not recognized.").Be.True();

			mappedType.Collections.Single()
				.Name.Should().Be.EqualTo(Reflector.PropertyName<PersonWithConvention>(x => x.Addresses));

			mappedType.References.Single()
				.Name.Should().Be.EqualTo(Reflector.PropertyName<PersonWithConvention>(x => x.Parent));
		}

		private abstract class PersonWithConvention
		{
			public string Name { get; set; }

			public PersonWithConvention Parent { get; set; }

			public IEnumerable<Address> Addresses { get; protected set; }
		}

		private class PersonWithConventionMetadata
		{
			[Required]
			public string Name { get; set; }

			public PersonWithMetadata Parent { get; set; }

			public IEnumerable<Address> Addresses { get; protected set; }
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