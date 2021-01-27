namespace EyeSoft.Core.Test.Helpers
{
    using System.Collections.Generic;

    internal class Category
	{
		private readonly IList<Category> children = new List<Category>();

		private Category(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }

		public Category Parent { get; set; }

		public IList<Category> Children
		{
			get { return children; }
		}

		public static Category CreateHierarchy()
		{
			return
				new Category("Root ")
					.AddChild(new Category(" Root 1 ").AddChild("Root 1.1 "))
					.AddChild(" Root 2");
		}

		public override string ToString()
		{
			return Name;
		}

		private Category AddChild(string name)
		{
			var child = new Category(name) { Parent = this };
			children.Add(child);

			return this;
		}

		private Category AddChild(Category child)
		{
			child.Parent = this;
			children.Add(child);

			return this;
		}
	}
}