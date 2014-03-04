namespace EyeSoft.Testing.IO
{
	using System.IO;

	using EyeSoft.IO;

	public class RandomFileInfo : FileInfoBase
	{
		private readonly string fullName;

		private readonly bool onlyLetters;

		public RandomFileInfo(string fullName, long length, bool onlyLetters)
			: base(fullName, length)
		{
			this.fullName = fullName;
			this.onlyLetters = onlyLetters;
		}

		public override Stream OpenRead()
		{
			return new RandomStream(Length, onlyLetters, fullName.GetHashCode());
		}
	}
}