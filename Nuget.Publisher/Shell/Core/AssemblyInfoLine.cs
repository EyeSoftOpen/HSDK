namespace EyeSoft.Nuget.Publisher.Shell
{
	public class AssemblyInfoLine
	{
		public AssemblyInfoLine(int index, string line)
		{
			Line = line;
			Index = index;
		}

		public int Index { get; private set; }

		public string Line { get; set; }

		public override string ToString()
		{
			return string.Format("{0}] {1}", Index.ToString().PadLeft(3), Line);
		}
	}
}