namespace EyeSoft.Nuget.Publisher.Shell.Workflow
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Build;

	public static class HsdkWorkflow
	{
		// ReSharper disable once MemberCanBePrivate.Global
		public const string SolutionPath = @"D:\Pw.Vs.com\Dc\Es.Hsdk\Main\EyeSoft.Hsdk.sln";

		// ReSharper disable once MemberCanBePrivate.Global
		public static readonly IEnumerable<string> PackagesId =
			new[]
				{
					"Accounting",
					"Accounting.Italian",
					"Accounting.Italian.Istat",
					"AutoMapper",
					"Core",
					"Data",
					////"Data.EntityFramework",
					////"Data.EntityFramework.Caching",
					////"Data.EntityFramework.Toolkit",
					////"Data.EntityFramework.Tracing",
					"Data.Nhibernate",
					////"Data.Raven",
					"Data.SqLite",
					"Domain",
					//"DynamicProxy",
					"FluentValidation",
					"ServiceLocator",
					"ServiceLocator.Windsor",
					"ServiceModel",
					//"ServiceStack.Text",
					//"SharpTests.Extensions",
					//"Shimmer.Client",
					"Web",
					"Windows",
					"Windows.Installer.InstallMate",
					"Windows.Model"
				}
			.Select(x => $"EyeSoft.{x}")
			.ToArray();

		public static RetrievePreviousVersionsStep GenerateBuildAndRevision()
		{
			var solutionFolderPath = new FileInfo(SolutionPath).Directory.FullName;

			var solutionSystemInfo = new SolutionSystemInfo(solutionFolderPath, SolutionPath);

			return new GenerateBuildAndRevisionStep(solutionSystemInfo, PackagesId).GenerateBuildAndRevision();
		}
	}
}