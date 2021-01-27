namespace EyeSoft.Accounting.Italian.FiscalCode.Parts
{
	public class AreaCode : Code
	{
		private readonly string code;

		public AreaCode(string code)
		{
			this.code = code;
		}

		protected internal override string GetCode()
		{
			return code;
		}
	}
}