namespace EyeSoft.Data.Raven.Test.Helpers
{
	public class Order
	{
		public Order(string id, int name)
		{
			Id = id;
			Name = name;
		}

		public string Id { get; set; }

		public int Name { get; set; }
	}
}