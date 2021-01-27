namespace EyeSoft.Core.Test.Helpers
{
    using System.Collections.Generic;

    internal class Customer
		: Person
	{
		public Customer(string name, string address)
		{
			Name = name;
			MainAddress = new Address(address, this);
			Addresses = new List<Address> { MainAddress };
		}

		public string Name { get; protected set; }

		public Address MainAddress { get; protected set; }

		public IEnumerable<Address> Addresses { get; protected set; }
	}
}