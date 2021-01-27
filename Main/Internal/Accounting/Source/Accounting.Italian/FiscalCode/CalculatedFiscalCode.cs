namespace EyeSoft.Accounting.Italian.FiscalCode
{
	public abstract class CalculatedFiscalCode
	{
		public string LastName { get; protected set; }

		public string FirstName { get; protected set; }

		public string Year { get; protected set; }

		public string Month { get; protected set; }

		public string Day { get; protected set; }

		public string Area { get; protected set; }

		public string Control { get; protected set; }

		public Sex Sex { get; protected set; }
	}
}