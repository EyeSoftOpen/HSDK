namespace EyeSoft.Nuget.Publisher.Shell
{
	public class BuildAndRevision
	{
		public BuildAndRevision(int build, int revision)
		{
			Build = build;
			Revision = revision;
		}

		public int Build { get; private set; }

		public int Revision { get; private set; }

		public override string ToString()
		{
			return $"{Build} {Revision}";
		}
	}
}