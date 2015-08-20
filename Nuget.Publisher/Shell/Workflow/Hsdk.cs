namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Build;

	public class HsdkWorkflow
	{
		public const string SolutionPath = @"D:\Pw.Vs.com\Dc\Es.Hsdk\Main\EyeSoft.Hsdk.sln";

		private static readonly IEnumerable<string> packagesId =
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
					//"Data.Raven",
					"Data.SqLite",
					"Domain",
					"DynamicProxy",
					"FluentValidation",
					"ServiceLocator",
					"ServiceLocator.Windsor",
					"ServiceModel",
					"ServiceStack.Text",
					"SharpTests.Extensions",
					"Shimmer.Client",
					"Web",
					"Windows",
					"Windows.Installer.InstallMate",
					"Windows.Model"
				}
			.Select(x => string.Concat("EyeSoft.", x))
			.ToArray();

		public RetrievePreviousVersionsStep GenerateBuildAndRevision()
		{
			var solutionFolderPath = new FileInfo(SolutionPath).Directory.FullName;

			var solutionSystemInfo = new SolutionSystemInfo(solutionFolderPath, SolutionPath);

			return new GenerateBuildAndRevisionStep(solutionSystemInfo, packagesId).GenerateBuildAndRevision();
		}
	}
}