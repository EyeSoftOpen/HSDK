namespace EyeSoft.Accounting.Italian.FiscalCode.Parts
{
    using System.Collections.Generic;
    using System.Linq;
    using EyeSoft.Collections.Generic;

    internal class EvenCode
	{
		private static readonly IDictionary<char, int> codes =
			new FluentDictionary<char, int>()
				.Entry('0', 1).Entry('1', 0).Entry('2', 5).Entry('3', 7).Entry('4', 9)
				.Entry('5', 13).Entry('6', 15).Entry('7', 17).Entry('8', 19).Entry('9', 21)
				.Entry('A', 1).Entry('B', 0).Entry('C', 5).Entry('D', 7).Entry('E', 9).Entry('F', 13)
				.Entry('G', 15).Entry('H', 17).Entry('I', 19).Entry('J', 21).Entry('K', 2)
				.Entry('L', 4).Entry('M', 18).Entry('N', 20).Entry('O', 11).Entry('P', 3)
				.Entry('Q', 6).Entry('R', 8).Entry('S', 12).Entry('T', 14).Entry('U', 16)
				.Entry('V', 10).Entry('W', 22).Entry('X', 25).Entry('Y', 24).Entry('Z', 23);

		public EvenCode(string partialCode)
		{
			var evenChars = partialCode.EvenChars();

			var sum =
				evenChars
					.Select(x => codes[char.ToUpper(x)])
					.Sum(x => x);

			Total = sum;
		}

		public int Total { get; }
	}
}