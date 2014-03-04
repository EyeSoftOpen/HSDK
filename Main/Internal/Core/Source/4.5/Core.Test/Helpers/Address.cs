namespace EyeSoft.Test.Helpers
{
	internal class Address
		: IEntity
	{
		public Address(string street, Customer customer)
		{
			Street = street;
			Customer = customer;
		}

		public string Street { get; protected set; }

		public Customer Customer { get; protected set; }
	}
}