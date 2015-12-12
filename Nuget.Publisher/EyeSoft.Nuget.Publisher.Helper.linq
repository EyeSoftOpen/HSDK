<Query Kind="Program">
  <Reference Relative="Core\bin\Debug\EyeSoft.Nuget.Publisher.Core.dll">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\EyeSoft.Nuget.Publisher.Core.dll</Reference>
  <Reference Relative="Shell\bin\Debug\EyeSoft.Nuget.Publisher.Shell.exe">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Shell\bin\Debug\EyeSoft.Nuget.Publisher.Shell.exe</Reference>
  <Reference Relative="Core\bin\Debug\Microsoft.Web.XmlTransform.dll">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\Microsoft.Web.XmlTransform.dll</Reference>
  <Reference Relative="Core\bin\Debug\NuGet.Core.dll">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\NuGet.Core.dll</Reference>
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
  <Namespace>EyeSoft.Nuget.Publisher.Shell.Nuget</Namespace>
  <Namespace>EyeSoft.Nuget.Publisher.Shell.Workflow</Namespace>
  <Namespace>Query</Namespace>
</Query>

void Main()
{
	Util.AutoScrollResults = true;

	NugetHelper.Pack();
}
}

namespace Query
{
	public static class NugetHelper
	{
		private static readonly DirectoryInfo solutionDirectory = new FileInfo(HsdkWorkflow.SolutionPath).Directory;
		private static readonly string nugetExePath = Path.Combine(solutionDirectory.FullName, @".nuget\NuGet.exe");
		private static readonly string nugetCompilePath = Path.Combine(solutionDirectory.FullName, "Nuget.Packages");

		public static void Pack()
		{
			var projectsPath = solutionDirectory
				.GetFiles("*.csproj", SearchOption.AllDirectories)
				.Where(x => x.Directory.GetFiles("*.nuspec").Any() && HsdkWorkflow.PackagesId.Contains(Path.GetFileNameWithoutExtension(x.Name)))
				.OrderBy(x => x.Name)
				.ToArray();

			Directory.CreateDirectory(nugetCompilePath);

			var packagesToPublish =
				projectsPath
					.Select(x =>
						new
						{
							ProjectFile = x,
							Package = Packages.Parse(x.Directory.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories).Single())
						});

			packagesToPublish
				.Select(x => new
				{
					x.Package.Title,
					Version = x.Package.PackageVersion.ToString(),
					DateTime = x.Package.PackageVersion.ToDateTime(),
					Publish = new Hyperlinq(() => Publish(x.Package), "Publish")
				})
				.Dump();

			foreach (var packageWithProjectFile in packagesToPublish.AsParallel())
			{
				var arguments = $"pack -Prop Configuration=Release \"{packageWithProjectFile.ProjectFile.FullName}\"";
				
				var compiledPackagePath = packageWithProjectFile.Package.ToFilePath();

				if (File.Exists(compiledPackagePath))
                {
					Console.WriteLine($"The pack {packageWithProjectFile.Package.Title} already exists, skypped.");
					continue;
				}
				
				Console.WriteLine($"Packing the file {Path.GetFileNameWithoutExtension(packageWithProjectFile.Package.Title)}...");

				ProcessHelper.Start(nugetExePath, arguments, nugetCompilePath, false);
			}
		}

		private static void Publish(Package package)
		{
			var arguments = $"push \"{package.ToFilePath()}\"";

			ProcessHelper.Start(nugetExePath, arguments, nugetCompilePath, true);
		}

		private static string ToFilePath(this Package package)
		{
			var fileName = $"{package.Title}.{package.PackageVersion.ToString()}.nupkg";
			
			var path = $"{Path.Combine(nugetCompilePath, fileName)}";

			return path;
		}
	}