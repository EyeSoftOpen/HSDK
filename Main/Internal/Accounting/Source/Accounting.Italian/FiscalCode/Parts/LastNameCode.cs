namespace EyeSoft.Accounting.Italian
{
	public class LastNameCode : NameCode
	{
		public LastNameCode(string name) : base(name)
		{
		}

		protected internal override string GetCode()
		{
			var lastnameConsonants = ExtractConsonants(name);
			var lastnameVocals = ExtractVocals(name);

			// The lastname has >= 3 consonants
			if (lastnameConsonants.Length >= 3)
			{
				return lastnameConsonants.Substring(0, 3);
			}

			// The lastname has >= 3 consonants and 3 vocals
			if (lastnameConsonants.Length + lastnameVocals.Length >= 3)
			{
				return lastnameConsonants + lastnameVocals.Substring(0, 3 - lastnameConsonants.Length);
			}

			// The lastname has <= 3 consonants and 3 vocals
			return (lastnameConsonants + lastnameVocals).PadRight(3, 'X');
		}
	}
}