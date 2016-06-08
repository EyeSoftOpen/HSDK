namespace EyeSoft.Nuget.Publisher.Core.Core
{
	using System;

	public class BuildAndRevision
	{
		public BuildAndRevision(int build, int revision)
		{
			Build = build;
			Revision = revision;

			try
			{
				Date = new DateTime(2000, 1, 1).AddDays(build).AddSeconds(revision * 2);
			}
			catch
			{
			}
		}

		public int Build { get; }

		public int Revision { get; }

		public DateTime? Date { get; }

		public override string ToString()
		{
			return Date.HasValue ? $"{Build} {Revision} {Date.Value}" : $"{Build} {Revision}";
		}
	}
}