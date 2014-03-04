namespace EyeSoft.Testing.Domain.Helpers.Domain5
{
	public class Invoice : AdministrativeDocument
	{
		protected internal Invoice()
		{
		}

		public virtual string Number { get; protected internal set; }

		public virtual InvoiceHead InvoiceHead { get; protected internal set; }
	}
}