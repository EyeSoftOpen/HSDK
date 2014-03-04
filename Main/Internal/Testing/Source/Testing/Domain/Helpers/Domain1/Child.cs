namespace EyeSoft.Testing.Domain.Helpers.Domain1
{
	using System;

	using EyeSoft.Domain;

	public class Child :
		Aggregate
	{
		protected Child()
		{
		}

		public virtual string Street { get; protected set; }

		public virtual School School { get; protected set; }

		public static Child Create(School customer, string street)
		{
			return
				new Child
					{
						Id = Guid.NewGuid(),
						School = customer,
						Street = street
					};
		}
	}
}