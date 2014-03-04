namespace EyeSoft.Testing.Domain.Helpers.Domain5
{
	public static class InvoiceValidator
	{
		public static void Validate(Invoice invoice)
		{
			Enforce.Argument(() => invoice);
		}
	}
}