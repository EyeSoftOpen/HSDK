using System;

namespace EyeSoft.Architecture.Prototype.Accounting.Commanding
{
	public class UpdateEstimateDescriptionCommand : IdentityCommand
	{
		public Guid EstimateId { get; set; }

		public string Description { get; set; }
	}
}