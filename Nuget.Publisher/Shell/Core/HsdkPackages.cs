namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.LinqPad;

	using Newtonsoft.Json;

	public static class HsdkPackages
	{
		private const string SolutionPath = @"D:\Picoware.VisualStudio.com\DefaultCollection\Es.Hsdk\Main";

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
				}.Select(x => string.Concat("EyeSoft.", x));

		public static IEnumerable<PackageWithFramework> GetAll()
		{
			var checkPackages =
				new DirectoryInfo(SolutionPath)
					.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories)
					.Select(Packages.Parse)
					.Where(x => x != null && packagesId.Contains(x.Title))
					.GroupBy(x => x.Title)
					.ToDictionary(k => k.Key, v => new { Versions = v.Select(y => y.PackageVersion), Packages = v.Select(y => y) })
					.Where(x => x.Value.Packages.Any(y => y.HasNuget))
					.ToArray();

			var packagesWithDifferentVersions = checkPackages.Where(x => x.Value.Versions.Distinct().Count() > 1).ToArray();

			if (packagesWithDifferentVersions.Any())
			{
				packagesWithDifferentVersions.Dump("Packages with different versions to be fixed");
				return null;
			}

			var packages =
				checkPackages.OrderBy(x => x.Key)
					.Select(x => new PackageWithFramework(x.Key, x.Value.Versions.First(), x.Value.Packages));

			return packages;
		}

		public static IReadOnlyDictionary<string, string> GetPreviousVersions()
		{
			var jsonPath = Path.Combine(SolutionPath, "Libraries", "Hsdk.Pachages.Version.json");

			var json = Storage.ReadAllText(jsonPath);

			var versions = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

			return new Dictionary<string, string>(versions);
		}
	}
}