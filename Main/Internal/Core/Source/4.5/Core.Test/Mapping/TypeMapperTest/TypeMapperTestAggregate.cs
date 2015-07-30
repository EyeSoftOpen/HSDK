namespace EyeSoft.Test.Mapping
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Mapping;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeMapperTestAggregate
	{
		[TestMethod]
		public void CheckMappingOfAggregate()
		{
			var mappedType =
				TypeMapperFactory.Create().Map<Blog>();

			mappedType.Primitives.Count().Should("Primitives member are retrieved. Expected none.").Be.EqualTo(1);
			mappedType.References.Count().Should("References member are retrieved. Expected none.").Be.EqualTo(0);
			var collection = mappedType.Collections.Single();

			collection.Name.Should().Be.EqualTo("PostList");
			collection.Type.Should().Be.EqualTo(typeof(IList<Post>));
		}

		// ReSharper disable once ClassNeverInstantiated.Local
		private class Blog
		{
			// ReSharper disable once UnusedMember.Local
			public string Name { get; set; }

			// ReSharper disable once UnusedMember.Local
			public IList<Post> PostList { get; set; }
		}

		// ReSharper disable once ClassNeverInstantiated.Local
		private class Post
		{
		}
	}
}