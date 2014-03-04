namespace EyeSoft.Testing.Domain.Helpers.Domain5
{
	public static class InvoiceFactory
	{
		public static Invoice ByCustomer(string customer, string invoiceNumber)
		{
			Enforce
				.Argument(() => customer)
				.Argument(() => invoiceNumber);

			var invoice = new Invoice { Customer = customer, Number = invoiceNumber };

			InvoiceValidator.Validate(invoice);

			return invoice;
		}
	}
}