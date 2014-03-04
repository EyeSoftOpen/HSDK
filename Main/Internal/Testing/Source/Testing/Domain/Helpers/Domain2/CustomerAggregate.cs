namespace EyeSoft.Testing.Domain.Helpers.Domain2
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.ComponentModel.DataAnnotations.Schema;
	using EyeSoft.Domain;

	public abstract class CustomerAggregate
		: Entity
	{
		[NotMapped]
		protected string Unmapped { get; set; }

		[Required]
		[StringLength(50)]
		protected string Name { get; set; }

		[Required]
		protected HeadOffice HeadOffice { get; set; }

		protected IEnumerable<Order> Orders { get; set; }

		private string FullName { get; set; }
	}
}