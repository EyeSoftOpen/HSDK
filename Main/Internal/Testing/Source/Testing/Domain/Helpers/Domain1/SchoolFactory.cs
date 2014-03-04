namespace EyeSoft.Testing.Domain.Helpers.Domain1
{
	using System;

	public static class SchoolFactory
	{
		public static School Create(string name)
		{
			return new School { Id = new Guid("93e6c9a2-0846-4bd2-b166-9efe3de0162b"), Name = name };
		}

		public static School Create(string name, string child)
		{
			var school = new School { Id = new Guid("ff1e7fc8-112a-42a7-a203-9b8e13902c16"), Name = name };
			school.AddChild(child);
			return school;
		}

		public static School Create(string name, string address1, string address2)
		{
			var school = new School { Id = new Guid("ff1e7fc8-112a-42a7-a203-9b8e13902c16"), Name = name };
			school.AddChild(address1);
			school.AddChild(address2);

			return school;
		}
	}
}