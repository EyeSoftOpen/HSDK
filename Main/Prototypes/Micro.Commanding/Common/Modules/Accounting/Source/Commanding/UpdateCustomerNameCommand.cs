using System;

namespace EyeSoft.Architecture.Prototype.Accounting.Commanding
{
	public class UpdateCustomerNameCommand : IdentityCommand
	{
		public Guid EstimateId { get; set; }

		public string CustomerName { get; set; }
	}
}