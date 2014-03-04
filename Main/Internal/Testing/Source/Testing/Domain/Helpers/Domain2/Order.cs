namespace EyeSoft.Testing.Domain.Helpers.Domain2
{
	using EyeSoft.Domain;

	public abstract class Order
		: Entity
	{
		protected int Price { get; set; }
	}
}