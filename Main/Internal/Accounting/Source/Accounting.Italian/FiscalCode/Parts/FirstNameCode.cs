namespace EyeSoft.Accounting.Italian.FiscalCode.Parts
{
	public class FirstNameCode : NameCode
	{
		internal FirstNameCode(string name) : base(name)
		{
		}

		protected internal override string GetCode()
		{
			var firstnameConsonants = ExtractConsonants(name);
			var firstnameVocals = ExtractVocals(name);

			// The firstname has >= 4 consonants
			if (firstnameConsonants.Length >= 4)
			{
				return firstnameConsonants.Substring(0, 1) + firstnameConsonants.Substring(2, 2);
			}

			// The firstname has >= 3 consonants and vocals
			if (firstnameConsonants.Length + firstnameVocals.Length >= 3)
			{
				return firstnameConsonants + firstnameVocals.Substring(0, 3 - firstnameConsonants.Length);
			}

			// The firstname has <= 3 consonants and 3 vocals
			return (firstnameConsonants + firstnameVocals).PadRight(3, 'X');
		}
	}
}