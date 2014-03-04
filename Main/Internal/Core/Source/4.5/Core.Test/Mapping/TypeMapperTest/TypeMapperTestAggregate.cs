namespace EyeSoft.Test.Mapping
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Mapping;
	using EyeSoft.Testing.Domain.Helpers.Domain4;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeMapperTestAggregate
	{
		[TestMethod]
		public virtual void CheckMappingOfAggregate()
		{
			var mappedType =
				TypeMapperFactory.Create().Map<Blog>();

			mappedType.Primitives.Count().Should("Primitives member are retrieved. Expected none.").Be.EqualTo(1);
			mappedType.References.Count().Should("References member are retrieved. Expected none.").Be.EqualTo(0);
			var collection = mappedType.Collections.Single();

			collection.Name.Should().Be.EqualTo("PostList");
			collection.Type.Should().Be.EqualTo(typeof(IList<Post>));
		}
	}
}