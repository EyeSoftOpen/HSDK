namespace Commanding
{
	using System;

	public class UpdateEstimateDescriptionCommand : IdentityCommand
	{
		public Guid EstimateId { get; set; }

		public string Description { get; set; }
	}
}