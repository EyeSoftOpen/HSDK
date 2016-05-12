namespace EyeSoft.Nuget.Publisher.Core.Core
{
	using System;

	using EyeSoft.Nuget.Publisher.Shell.Core;

	public static class VersionHelper
	{
		private static readonly DateTime referenceDateTime = new DateTime(2000, 1, 1);

		public static Version Increment(this Version version, DateTime? dateTime = null)
		{
			return Increment(version.Major, version.Minor, dateTime);
		}

		public static Version Increment(this Version version, BuildAndRevision buildAndRevision)
		{
			return Increment(version.Major, version.Minor, buildAndRevision);
		}

		public static Version Increment(int major, int minor, DateTime? dateTime)
		{
			var buildAndRevision = GenerateBuildAndRevision(dateTime);

			return Increment(major, minor, buildAndRevision);
		}

		public static Version Increment(int major, int minor, BuildAndRevision buildAndRevision)
		{
			return new Version(major, minor, buildAndRevision.Build, buildAndRevision.Revision);
		}

		public static BuildAndRevision GenerateBuildAndRevision(DateTime? dateTime = null)
		{
			var localDateTime = dateTime ?? DateTime.UtcNow;

			var build = localDateTime.Date.Subtract(referenceDateTime).TotalDays;
			var revision = localDateTime.Subtract(DateTime.Today).TotalSeconds / 2;

			////return new BuildAndRevision(5976, 28727);
			return new BuildAndRevision((int)build, (int)revision);
		}

		// ReSharper disable once UnusedMember.Global
		public static DateTime ToDateTime(this Version version)
		{
			return referenceDateTime.AddDays(version.Build).AddSeconds(version.Revision * 2);
		}
	}
}