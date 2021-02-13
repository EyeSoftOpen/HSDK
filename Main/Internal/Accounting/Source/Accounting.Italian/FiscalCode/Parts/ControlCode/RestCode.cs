namespace EyeSoft.Accounting.Italian.FiscalCode.Parts
{
    using System.Collections.Generic;
    using System.Linq;
    using EyeSoft.Collections.Generic;
    using EyeSoft.Extensions;

    internal class RestCode
	{
		private readonly string value;

		public RestCode(int sum)
		{
			var rest = sum % 26;

			value = CodiciResto().Single(x => x.Value == rest).Key;
		}

		public override string ToString()
		{
			return value;
		}

		private IEnumerable<KeyValuePair<string, int>> CodiciResto()
		{
			var tabellaConversione = new FluentDictionary<string, int>();
			var i = 0;

			Enumerable
				.Range('A', 'Z' - 'A' + 1)
				.ForEach(x => tabellaConversione.Entry(((char)x).ToInvariant(), i++));

			return tabellaConversione;
		}
	}
}