namespace EyeSoft.Core.Test.Mapping
{
    using System.Collections.Generic;
    using System.Linq;
    using EyeSoft.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class TypeMapperTestAggregate
	{
		[TestMethod]
		public void CheckMappingOfAggregate()
		{
			var mappedType =
				TypeMapperFactory.Create().Map<Blog>();

			mappedType.Primitives.Count().Should().Be(1, "Primitives member are retrieved. Expected none.");
			mappedType.References.Count().Should().Be(0, "References member are retrieved. Expected none.");
			var collection = mappedType.Collections.Single();

			collection.Name.Should().Be("PostList");
			collection.Type.Should().Be(typeof(IList<Post>));
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