namespace EyeSoft.Core
{
    using System.Collections.Generic;

    public class TokenDictionary
	{
		private readonly string value;

		private readonly IDictionary<int, string> tokens = new Dictionary<int, string>();

		private int position;

		public TokenDictionary(string value, int tokenNumber, int length)
		{
			this.value = value;
			Tokens = new IndexedTokens(tokens);

			Token(tokenNumber, length);
		}

		public IndexedTokens Tokens { get; private set; }

		public TokenDictionary Token(int tokenNumber, int length)
		{
			tokens.Add(tokenNumber, value.Substring(position, length));
			position += length;

			return this;
		}
	}
}