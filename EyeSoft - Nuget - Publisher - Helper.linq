<Query Kind="Program">
  <Reference Relative="Nuget.Publisher\Core\bin\Debug\Microsoft.Web.XmlTransform.dll">D:\Es.Github\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\Microsoft.Web.XmlTransform.dll</Reference>
  <Reference Relative="Nuget.Publisher\Core\bin\Debug\NuGet.Core.dll">D:\Es.Github\Es.Hsdk\Nuget.Publisher\Core\bin\Debug\NuGet.Core.dll</Reference>
  <NuGetReference>Castle.WcfIntegrationFacility</NuGetReference>
  <NuGetReference>Castle.Windsor</NuGetReference>
  <NuGetReference>De.TorstenMandelkow.MetroChart</NuGetReference>
  <NuGetReference>DotNetZip</NuGetReference>
  <NuGetReference>EyeSoft.Core</NuGetReference>
  <NuGetReference>HtmlAgilityPack</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Client</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>System.Data.SQLite.x64</NuGetReference>
  <Namespace>EyeSoft.IO</Namespace>
  <Namespace>NuGet</Namespace>
  <Namespace>Query</Namespace>
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	Util.AutoScrollResults = true;

	/// 1 - Set version of updated projects on HSDK solution to same version
	/// 2 - Set newVersion to same value
	/// 3 - Run script
	/// NOTE If versions are not equals check if version of projects for different FW on solution if are equals!!
	
	var newVersion = new Version("3.0.6596.33990");
	
	NugetHelper.Pack(true, newVersion);
}
}

namespace Query
{
	public static class NugetHelper
    {
        private static readonly DirectoryInfo solutionDirectory = new FileInfo(HsdkWorkflow.SolutionPath).Directory;
        private static readonly string nugetExePath = Path.Combine(solutionDirectory.FullName, @".nuget\NuGet.exe");
        private static readonly string nugetCompilePath = Path.Combine(solutionDirectory.FullName, "Nuget.Packages");

        public static void Pack(bool buildSolution, Version newVersion)
        {
            var solutionPath = solutionDirectory.FullName;
            var projectPath = Path.Combine(solutionPath, "EyeSoft.Hsdk.sln");

            var projectsPath = solutionDirectory
                .GetFiles("*.csproj", SearchOption.AllDirectories)
                .Where(x => x.Directory.GetFiles("*.nuspec").Any() && HsdkWorkflow.PackagesId.Contains(Path.GetFileNameWithoutExtension(x.Name)))
                .OrderBy(x => x.Name)
                .ToArray();

            var solutionPackages =
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
                        })
                        .ToArray();

            var packages = solutionPackages.ToDictionary(k => k.Package.Title, v => (Func<Version>)(() => v.Package.PackageVersion));

            foreach (var package in solutionPackages)
            {
                 package.Package.Title.Dump("Dependecies and versione updating");

                var updated = package.Package.TryUpdateNuspecDependencies(packages);

                if (updated.Any())
                {
                    package.Package.IncrementAssemblyInfo(newVersion);
                    package.Package.Title.Dump("Dependecies and versione updated");
                }
            }

            if (buildSolution)
            {
                new MsBuild(projectPath, solutionPath, true).Build();
            }

            var packagesToPublishWithVersions =
                solutionPackages
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
                                         x.Package,
                                         x.Version,
                                         x.DateTime,
                                         AssemblyVersionsAreEqual = x.AssemblyVersions.AreEquals,
                                         AssemblyVersions = x.AssemblyVersions.AreEquals ? null : x.AssemblyVersions
                                     })
                    .Select(x => new
                                     {
                                         Result = x.AssemblyVersionsAreEqual ? LinqPadUtil.FontAwesomeIcon("fa-flag", "#8dc63f") : LinqPadUtil.FontAwesomeIcon("fa-flag", "#cc3333"),
                                         x.Title,
                                         x.Version,
                                         x.DateTime,
                                         x.AssemblyVersionsAreEqual,
                                         x.AssemblyVersions,
                                         x.Package
                                     })
                    .OrderByDescending(x => x.AssemblyVersionsAreEqual);

            if (!buildSolution)
            {
                packagesToPublishWithVersions
                    .Select(x => new
                                     {
                                         x.Result,
                                         x.Title,
                                         x.Version,
                                         x.DateTime,
                                         x.AssemblyVersions
					})
				 .Dump($"To allow the publish change the '{nameof(buildSolution)}' paremeter to true", 1);

				return;
			}

			packagesToPublishWithVersions
				.Select(
					x => new
					{
						x.Result,
						x.Title,
						Publish = x.AssemblyVersions != null ? (object)"Versions mismatch" : new Hyperlinq(() => Publish(x.Package), "Publish"),
						x.Version,
						x.DateTime,
						x.AssemblyVersions
					})
				.Dump(1);

			Directory.CreateDirectory(nugetCompilePath);

			nugetCompilePath.Dump();

			foreach (var packageWithProjectFile in solutionPackages.AsParallel())
			{
				var arguments = $"pack -Prop Configuration=Release \"{packageWithProjectFile.ProjectFile.FullName}\"";

				var compiledPackagePath = packageWithProjectFile.Package.ToFilePath();

				if (File.Exists(compiledPackagePath))
				{
					Console.WriteLine($"The pack {packageWithProjectFile.Package.Title} already exists, skypped.");
					continue;
				}

				Console.WriteLine($"Packing the file {packageWithProjectFile.Package.Title}...");

				ProcessHelper.Start(nugetExePath.Dump("nugetExePath"), arguments.Dump("arguments"), nugetCompilePath.Dump("nugetCompilePath"), true);
			}

			new Hyperlinq(() => solutionPackages.Select(x => x.Package).ToList().ForEach(x => Publish(x)), "Publish all packages").Dump();
		}

		private static void Publish(Package package)
		{
			var arguments = $"push \"{package.ToFilePath()}\" -Source https://api.nuget.org/v3/index.json";

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
		public string FileVersion { get; }
		public bool AreEquals { get; }
	}

	public static class HsdkWorkflow
	{
		// ReSharper disable once MemberCanBePrivate.Global
		public const string SolutionPath = @"D:\Es.Github\Es.Hsdk\Main\EyeSoft.Hsdk.sln";


		// ReSharper disable once MemberCanBePrivate.Global
		public static readonly IEnumerable<string> PackagesId =
			new[]
				{
					"Accounting",
					"Accounting.Italian",
					"Accounting.Italian.Istat",
					"ActiveCampaign",
					"AutoMapper",
					"Core",
					"Data",
					//"Data.EntityFramework",
					//"Data.EntityFramework.Caching",
					//"Data.EntityFramework.Toolkit",
					//"Data.EntityFramework.Tracing",
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
	}

	public static class Packages
	{
		public static Package Parse(FileInfo assemblyInfoFile)
		{
			try
			{
				var lines = Storage.ReadAllLines(assemblyInfoFile.FullName);

				var title = AssemblyInfo.GetData(lines, AssemblyInfoData.AssemblyTitle);

				var projectFile = assemblyInfoFile.Directory.Parent.GetFiles(title + "*.csproj").SingleOrDefault();

				if (projectFile == null)
				{
					return null;
				}

				var targetFramework = ExtractTargetFramework(projectFile);

				var version = AssemblyInfo.GetData(lines, AssemblyInfoData.AssemblyVersion);

				var nuspec = projectFile.Directory.GetFiles("Package.nuspec").SingleOrDefault();

				if (nuspec == null)
				{
					return new Package(projectFile, assemblyInfoFile, lines, title, version, targetFramework);
				}

				return new Package(projectFile, assemblyInfoFile, lines, title, version, targetFramework, nuspec);
			}
			catch (Exception exception)
			{
				throw new IOException($"Cannot parse the file {assemblyInfoFile.FullName}.", exception);
			}
		}

		private static string ExtractTargetFramework(FileInfo projectFile)
		{
			var targetFrameworkElement =
				XElement.Load(projectFile.FullName)
					.Descendants("{http://schemas.microsoft.com/developer/msbuild/2003}TargetFrameworkVersion")
					.SingleOrDefault();

			var targetFramework = "4.0";

			if (targetFrameworkElement != null)
			{
				targetFramework = targetFrameworkElement.Value.Replace("v", null);
			}
			return targetFramework;
		}
	}

	public static class Storage
	{
		private static readonly bool canWrite = true;

		static Storage()
		{
			////var choice = Util.ReadLine("Overwrite all Nuspec and AssemblyInfo files for real? [Y, N]");
			////canWrite = choice.Equals("y", StringComparison.InvariantCultureIgnoreCase);
		}

		public static string[] ReadAllLines(string path)
		{
			return File.ReadAllLines(path);
		}

		public static string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}

		public static void WriteAllText(string path, string contents)
		{
			if (!canWrite)
			{
				////ConsoleHelper.WriteLine("Simulated write of contents in {0} file.", path);
				return;
			}

			File.WriteAllText(path, contents);
		}

		public static void WriteAllLines(string path, string[] contents)
		{
			if (!canWrite)
			{
				////ConsoleHelper.WriteLine("Simulated write of contents in {0} file.", path);
				return;
			}

			File.WriteAllLines(path, contents);
		}

		public static void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}
	}

	public static class AssemblyInfo
	{
		public static string GetData(IEnumerable<string> lines, AssemblyInfoData data)
		{
			var assemblyLine = GetAssemblyLine(data);

			var line = lines.SingleOrDefault(x => x.StartsWith(assemblyLine));

			if (line == null)
			{
				return null;
			}

			return GetDataFromLine(assemblyLine, line);
		}

		public static string GetData(string line, AssemblyInfoData data)
		{
			var assemblyLine = GetAssemblyLine(data);

			return GetDataFromLine(assemblyLine, line);
		}

		public static T GetData<T>(string line, AssemblyInfoData data)
		{
			return Converter.Convert<T>(GetData(line, data));
		}

		private static string GetDataFromLine(string assemblyLine, string line)
		{
			line = line.Replace(assemblyLine, null).Replace("\")]", null);

			return line;
		}

		private static string GetAssemblyLine(AssemblyInfoData data)
		{
			var assemblyLine = $"[assembly: {data}(\"";

			return assemblyLine;
		}
	}

	public enum AssemblyInfoData
	{
		AssemblyVersion,
		AssemblyFileVersion,
		AssemblyTitle
	}

	public static class Converter
	{
		public static T Convert<T>(object value)
		{
			var type = typeof(T);

			var typeDescriptor = TypeDescriptor.GetConverter(type);

			T result;

			if (typeDescriptor.CanConvertTo(type))
			{
				result = (T)typeDescriptor.ConvertTo(value, type);
				return result;
			}

			var parseMethod = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);

			if (parseMethod == null)
			{
				throw new InvalidOperationException("Cannot use a TypeDescriptor or a Parse method to convert the object.");
			}

			try
			{
				result = (T)parseMethod.Invoke(null, new[] { value });
				return result;
			}
			catch
			{
				throw;
			}

		}
	}
	
	public class Package
	{
		private readonly string[] lines;

		public Package(FileInfo projectFile, FileInfo assemblyInfoFile, string[] lines, string title, string version, string targetFramework, FileInfo nuspecFile)
			: this(projectFile, assemblyInfoFile, lines, title, version, targetFramework)
		{
			NuspecFile = nuspecFile;
			HasNuget = true;
		}

		public Package(FileInfo projectFile, FileInfo assemblyInfoFile, string[] lines, string title, string version, string targetFramework)
		{
			ProjectFile = projectFile;
			AssemblyInfoFile = assemblyInfoFile;
			this.lines = lines;

			Title = title;
			PackageVersion = Version.Parse(version);
			TargetFramework = new Hyperlinq(() => ProcessHelper.Start(AssemblyInfoFile.Directory.Parent.FullName), targetFramework);

////			TargetFramework = Version.Parse(targetFramework).ToString();
		}

		public FileInfo ProjectFile { get; }

		public FileInfo NuspecFile { get; }

		public FileInfo AssemblyInfoFile { get; }

		public string Title { get; }

		public Version PackageVersion { get; }

		public Hyperlinq TargetFramework { get; }

		public bool HasNuget { get; }

		public IEnumerable<PackageUpdate> TryUpdateNuspecDependencies(IReadOnlyDictionary<string, Func<Version>> packages)
		{
			var nuspec = File.ReadAllText(NuspecFile.FullName);

			nuspec = nuspec.Replace("$", "__");

			using (var readStream = new MemoryStream(Encoding.Default.GetBytes(nuspec)))
			{
				var manifest = Manifest.ReadFrom(readStream, false);

				using (var writeStream = new MemoryStream())
				{
					var dependencies = manifest.Metadata.DependencySets.SingleOrDefault();

					if (dependencies == null)
					{
						return Enumerable.Empty<PackageUpdate>();
					}

					var dependenciesToUpdate =
						dependencies.Dependencies
							.Where(x => packages.ContainsKey(x.Id) && (packages[x.Id]() != new Version(x.Version)))
							.ToArray();

					var updates = new List<PackageUpdate>();

					foreach (var dependencyToUpdate in dependenciesToUpdate)
					{
						var currentVersion = packages[dependencyToUpdate.Id]().ToString();

						updates.Add(new PackageUpdate(dependencyToUpdate.Id, dependencyToUpdate.Version, currentVersion));

						dependencyToUpdate.Version = currentVersion;
					}

					manifest.Save(writeStream);

					nuspec = Encoding.Default.GetString(writeStream.ToArray());

					nuspec = nuspec.Replace("__", "$");

					Storage.WriteAllText(NuspecFile.FullName, nuspec);

					return updates;
				}
			}
		}

		internal void UpdateNuspecVersion(Version version)
		{
			var nuspec = File.ReadAllText(NuspecFile.FullName);

			nuspec = nuspec.Replace("$", "__");

			using (var readStream = new MemoryStream(Encoding.Default.GetBytes(nuspec)))
			{
				var manifest = Manifest.ReadFrom(readStream, false);

				using (var writeStream = new MemoryStream())
				{
					manifest.Metadata.Version = version.ToString();

					manifest.Save(writeStream);

					nuspec = Encoding.Default.GetString(writeStream.ToArray());

					nuspec = nuspec.Replace("__", "$");

					Storage.WriteAllText(NuspecFile.FullName, nuspec);
				}
			}
		}

		public void IncrementAssemblyInfo(Version newVersion)
		{
			var assemblyInfoLines =
				lines
					.Select((line, index) => new AssemblyInfoLine(index, line))
					.ToArray();

			UpdateAssemblyVersionLine(newVersion, assemblyInfoLines, AssemblyInfoData.AssemblyVersion);
			UpdateAssemblyVersionLine(newVersion, assemblyInfoLines, AssemblyInfoData.AssemblyFileVersion);

			Storage.WriteAllLines(AssemblyInfoFile.FullName, assemblyInfoLines.Select(x => x.Line).ToArray());
		}

		public override string ToString()
		{
			return $"{Title} {PackageVersion} FW {TargetFramework.Text}";
		}

		private static void UpdateAssemblyVersionLine(
			Version newVersion,
			IReadOnlyList<AssemblyInfoLine> assemblyInfoLines,
			AssemblyInfoData assemblyInfo)
		{
			var assemblyInfoVersion = assemblyInfo.ToString();

			var assemblyVersion = assemblyInfoLines.Single(x => x.Line.Contains(assemblyInfoVersion));

			var assemblyVersionLine = assemblyVersion.Line;

			var version = AssemblyInfo.GetData(assemblyVersion.Line, assemblyInfo);

			assemblyInfoLines[assemblyVersion.Index].Line = assemblyVersionLine.Replace(version, newVersion.ToString());
		}
	}

	public class PackageUpdate
	{
		public PackageUpdate(string id, string previous, string current)
		{
			Id = id;
			Previous = previous;
			Current = current;
		}

		public string Id { get; }

		public string Previous { get; }

		public string Current { get; }

		public override string ToString()
		{
			return $"{Id} {Previous} {Current}";
		}
	}
	
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