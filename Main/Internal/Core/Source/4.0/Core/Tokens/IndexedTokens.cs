namespace EyeSoft
{
	using System.Collections.Generic;

	public class IndexedTokens
	{
		private readonly IDictionary<int, string> tokens;

		public IndexedTokens(IDictionary<int, string> tokens)
		{
			this.tokens = tokens;
		}

		public string this[object index]
		{
			get
			{
				return tokens[(int)index];
			}
		}
	}
}