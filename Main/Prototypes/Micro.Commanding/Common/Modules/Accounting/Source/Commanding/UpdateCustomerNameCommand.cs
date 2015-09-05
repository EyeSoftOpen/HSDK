namespace Commanding
{
	using System;

	public class UpdateCustomerNameCommand : IdentityCommand
	{
		public Guid EstimateId { get; set; }

		public string CustomerName { get; set; }
	}
}