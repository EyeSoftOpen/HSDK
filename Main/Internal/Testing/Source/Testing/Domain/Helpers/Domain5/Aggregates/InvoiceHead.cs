namespace EyeSoft.Testing.Domain.Helpers.Domain5
{
	using System;
	using System.Globalization;

	using EyeSoft.Domain;

	public class InvoiceHead
		: Entity
	{
		protected internal InvoiceHead()
		{
			DocumentId = DateTime.Now.ToString(CultureInfo.InvariantCulture);
		}

		public virtual string DocumentId { get; protected internal set; }
	}
}