namespace EyeSoft.Nuget.Publisher.Shell
{
	using global::System;

	public static class VersionHelper
	{
		public static Version Increment(this Version version)
		{
			return Increment(version.Major, version.Minor);
		}

		public static Version Increment(this Version version, BuildAndRevision buildAndRevision)
		{
			return Increment(version.Major, version.Minor, buildAndRevision);
		}

		public static Version Increment(int major, int minor)
		{
			var buildAndRevision = GenerateBuildAndRevision();

			return Increment(major, minor, buildAndRevision);
		}

		public static Version Increment(int major, int minor, BuildAndRevision buildAndRevision)
		{
			return new Version(major, minor, (int)buildAndRevision.Build, (int)buildAndRevision.Revision);
		}

		public static BuildAndRevision GenerateBuildAndRevision()
		{
			var build = (DateTime.Today - new DateTime(2000, 1, 1)).TotalDays;
			var revision = (DateTime.Now - DateTime.Today).TotalSeconds / 2;

			return new BuildAndRevision((int)build, (int)revision);
		}
	}
}