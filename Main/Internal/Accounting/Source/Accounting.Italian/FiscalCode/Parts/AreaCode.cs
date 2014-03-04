namespace EyeSoft.Accounting.Italian
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