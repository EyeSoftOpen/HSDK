namespace EyeSoft.Core.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using EyeSoft.Normalization;
    using EyeSoft.Reflection;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ObjectTreeTest
	{
        [TestMethod]
        public void GetStringProperties()
        {
            var parent = Category.CreateHierarchy();

            var properties = new DefaultNormalizer().GetStringProperties(parent).ToArray();

            properties.Select(x => x.Name).Should().BeSameAs(new[] { nameof(Category.Name) });
        }

		[TestMethod]
		public void TraverseReferences()
		{
			var parent = Category.CreateHierarchy();

			var actionReferences = new List<string>();

			var references = ObjectTree.Traverse(
				parent,
				x =>
				{
					Normalizer.TrimProperties(x);
					actionReferences.Add(((Category)x).Name);
				});

			var names = new[] { "Root", "Root 1", "Root 1.1", "Root 2" };
			actionReferences.Should().BeSameAs(names);
			references.Cast<Category>().Select(x => x.Name).Should().BeSameAs(names);
		}
	}
}