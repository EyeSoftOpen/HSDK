namespace EyeSoft.Data.Raven.Test.Helpers
{
	using EyeSoft.Domain;

	public class Customer : Aggregate<string>
	{
		public Customer(string id, string name)
		{
			Id = id;
			Name = name;
		}

		public string Name { get; private set; }
	}
}