namespace EyeSoft.Test
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Normalization;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ObjectTreeTest
	{
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
			actionReferences.Should().Have.SameSequenceAs(names);
			references.Cast<Category>().Select(x => x.Name).Should().Have.SameSequenceAs(names);
		}
	}
}