namespace EyeSoft.Testing.Domain.Helpers.Domain1
{
	public static class Known
	{
		public static class Schools
		{
			public const string Name = "School1";

			public static School School
			{
				get
				{
					return SchoolFactory.Create(Name);
				}
			}

			public static School SchoolWithOneChild
			{
				get
				{
					return SchoolFactory.Create(Name, Children.Child1FirstName);
				}
			}

			public static School SchoolWithTwoChildren
			{
				get
				{
					return SchoolFactory.Create(Name, Children.Child1FirstName, Children.Child2FirstName);
				}
			}
		}

		public static class Children
		{
			public const string Child1FirstName = "Charles";

			public const string Child2FirstName = "Jamie";
		}
	}
}