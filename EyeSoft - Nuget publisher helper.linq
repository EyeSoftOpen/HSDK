<Query Kind="Program">
  <Reference Relative="Nuget.Publisher\Core\bin\Debug\EyeSoft.Nuget.Publisher.Core.dll">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\EyeSoft.Nuget.Publisher.Core.dll</Reference>
  <Reference Relative="Nuget.Publisher\Shell\bin\Debug\EyeSoft.Nuget.Publisher.Shell.exe">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Shell\bin\Debug\EyeSoft.Nuget.Publisher.Shell.exe</Reference>
  <Reference Relative="Nuget.Publisher\Core\bin\Debug\Microsoft.Web.XmlTransform.dll">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\Microsoft.Web.XmlTransform.dll</Reference>
  <Reference Relative="Nuget.Publisher\Core\bin\Debug\NuGet.Core.dll">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\NuGet.Core.dll</Reference>
  <NuGetReference>Castle.WcfIntegrationFacility</NuGetReference>
  <NuGetReference>Castle.Windsor</NuGetReference>
  <NuGetReference>De.TorstenMandelkow.MetroChart</NuGetReference>
  <NuGetReference>DotNetZip</NuGetReference>
  <NuGetReference>EyeSoft.Core</NuGetReference>
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Client</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>System.Data.SQLite.x64</NuGetReference>
  <Namespace>EyeSoft.Nuget.Publisher.Core.Core</Namespace>
  <Namespace>EyeSoft.Nuget.Publisher.Core.Nuget</Namespace>
  <Namespace>EyeSoft.Nuget.Publisher.Core.Workflow</Namespace>
  <Namespace>Query</Namespace>
</Query>

void Main()
{
	Util.AutoScrollResults = true;

	NugetHelper.Pack(false);
}
}

namespace Query
{
	public static class NugetHelper
	{
		private static readonly DirectoryInfo solutionDirectory = new FileInfo(HsdkWorkflow.SolutionPath.Dump()).Directory;
		private static readonly string nugetExePath = Path.Combine(solutionDirectory.FullName, @".nuget\NuGet.exe");
		private static readonly string nugetCompilePath = Path.Combine(solutionDirectory.FullName, "Nuget.Packages");

		public static void Pack(bool buildSolution)
		{
			var solutionPath = solutionDirectory.FullName;
			var projectPath = Path.Combine(solutionPath, "EyeSoft.Hsdk.sln");

			if (buildSolution)
			{
				new MsBuild(projectPath, solutionPath, true).Build();
			}

			var projectsPath = solutionDirectory
				.GetFiles("*.csproj", SearchOption.AllDirectories)
				.Where(x => x.Directory.GetFiles("*.nuspec").Any() && HsdkWorkflow.PackagesId.Contains(Path.GetFileNameWithoutExtension(x.Name)))
				.OrderBy(x => x.Name)
				.ToArray();

			var packagesToPublish =
				projectsPath
					.Select(x =>
						new
						{
							ProjectFile = x,
							Package = Packages.Parse(x.Directory.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories).Single())
						})
					.Select(x => new
					{
						x.ProjectFile,
						x.Package,
						AssemblyVersions = new AssemblyVersions(x.Package.PackageVersion, new DirectoryInfo(Path.Combine(x.ProjectFile.DirectoryName, "Bin", "Release")).GetFiles($"{x.Package.Title}.dll").Single().FullName)
					});


			var packagesToPublishWithVersions =
				packagesToPublish
					.Select(x => new
					{
						x.Package.Title,
						x.Package,
						Version = x.Package.PackageVersion.ToString(),
						DateTime = x.Package.PackageVersion.ToDateTime(),
						x.AssemblyVersions
					})
					.Select(x => new
					{
						x.Title,
						x.Version,
						x.DateTime,
						Publish = !x.AssemblyVersions.AreEquals ? (object)"Versions mismatch" : new Hyperlinq(() => Publish(x.Package), "Publish"),
						AssemblyVersionsAreEqual = x.AssemblyVersions.AreEquals,
						x.AssemblyVersions
					})
					.Select(x => new
					{
						Result = x.AssemblyVersionsAreEqual ? Util.Highlight("      ", "#00a300") : Util.Highlight("      ", "#ee1111"),
						x.Title,
						x.Version,
						x.DateTime,
						x.Publish,
						x.AssemblyVersionsAreEqual,
						x.AssemblyVersions
					})
					.OrderByDescending(x => x.AssemblyVersionsAreEqual)
					.Dump(1);

			if (!buildSolution)
			{
				return;
			}

			Directory.CreateDirectory(nugetCompilePath);

			foreach (var packageWithProjectFile in packagesToPublish.AsParallel())
			{
				var arguments = $"pack -Prop Configuration=Release \"{packageWithProjectFile.ProjectFile.FullName}\"";

				var compiledPackagePath = packageWithProjectFile.Package.ToFilePath();

				if (File.Exists(compiledPackagePath))
				{
					Console.WriteLine($"The pack {packageWithProjectFile.Package.Title} already exists, skypped.");
					continue;
				}

				Console.WriteLine($"Packing the file {packageWithProjectFile.Package.Title}...");

				ProcessHelper.Start(nugetExePath, arguments, nugetCompilePath, true);
			}

			new Hyperlinq(() => packagesToPublish.Select(x => x.Package).ToList().ForEach(x => Publish(x)), "Publish all packages").Dump();
		}

		private static void Publish(Package package)
		{
			var arguments = $"push \"{package.ToFilePath()}\" -Source https://www.nuget.org/api/v2/package";

			ProcessHelper.Start(nugetExePath, arguments, nugetCompilePath, true);
		}

		private static string ToFilePath(this Package package)
		{
			var fileName = $"{package.Title}.{package.PackageVersion.ToString()}.nupkg";

			var path = $"{Path.Combine(nugetCompilePath, fileName)}";

			return path;
		}
	}

	public class AssemblyVersions
	{
		public AssemblyVersions(Version packageVersion, string assemblyPath)
		{
			AssemblyVersion = Assembly.LoadFile(assemblyPath).GetName().Version.ToString();
			FileVersion = FileVersionInfo.GetVersionInfo(assemblyPath).FileVersion;
			
			AreEquals = (packageVersion == new Version(AssemblyVersion)) && (packageVersion == new Version(FileVersion));
		}

		public string AssemblyVersion { get; }
		public string FileVersion  { get; }
		public bool AreEquals  { get; }
	}