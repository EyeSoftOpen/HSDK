namespace EyeSoft.Accounting.Italian
{
	using System.Text.RegularExpressions;

	public abstract class NameCode : Code
	{
		protected readonly string name;

		private const string Vocals = "aeiouAEIOU";

		protected internal NameCode(string name)
		{
			this.name = PurifiesAccentsAndSpaces(name);
		}

		protected string ExtractConsonants(string stringa)
		{
			return Regex.Replace(stringa, $"[{Vocals}\\s]", string.Empty);
		}

		protected string ExtractVocals(string stringa)
		{
			return Regex.Replace(stringa, $"[^{Vocals}]", string.Empty);
		}

		private string PurifiesAccentsAndSpaces(string name)
		{
			return
				name
					.ToLower()
					.Replace(' ', '\0')
					.Replace('è', 'e')
					.Replace('à', 'a')
					.Replace('ù', 'u')
					.Replace('ò', 'o')
					.Replace('ì', 'i');
		}
	}
}