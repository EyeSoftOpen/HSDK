<Query Kind="Program">
  <DisableMyExtensions>true</DisableMyExtensions>
</Query>

void Main()
{
	Util.AutoScrollResults = true;
	
	var nugetHelper = new HsdkNugetHelper();
	
	var newVersion = nugetHelper.GetNewVersion().Dump("New version");
	
	nugetHelper.BuildAndPackAndPushAll();
	
	//nugetHelper.CopyAllPackagesToFolderForPublish(false);
	//nugetHelper.PushAllPackages();
	//nugetHelper.ShowAllVersions();
	//return;
	//CopyAllPackagesToFolderForPublish(basePath, true);
	
	var newVersionText = "3.0.7736.24658";
	var oldYears = "2020";
	//return;
	//new[]
	//{
	//	"3.0.7713.28810",
	//	"3.0.7714.20388",
	//	"3.0.6836.35031",
	//	"3.0.7349.34482",
	//	"3.0.7333.34942"
	//	}
	//	.ToList()
	//	.ForEach(x => ReplaceOldVersionWithNewVersion(basePath, x, newVersionText, oldYears));

	//BuildTheSolution(basePath);

	//CopyAllPackagesToFolderForPublish(basePath, true);
	//return;

	//nugetHelper.BuildProject("EyeSoft.Core");
	//nugetHelper.CopyAllPackagesToFolderForPublish(false);
}

#region HSDK
#endregion
public class HsdkNugetHelper : NugetHelper
{
	public HsdkNugetHelper() : base(@"D:\GitHub\HSDK\Main", "EyeSoft.Hsdk.sln", GetPackages())
	{
		
	}

	private static IEnumerable<Package> GetPackages()
	{
		var packages = new[]
		{
			//new Package("EyeSoft.Accounting"),
			//new Package("EyeSoft.Accounting.Italian"),
			//new Package("EyeSoft.Accounting.Italian.Istat"),
			//new Package("EyeSoft.ActiveCampaign", "4.5"),
			//new Package("EyeSoft.AutoMapper"),
			new Package("EyeSoft.Core", "4.0"),
			//new Package("EyeSoft.Data"),
			//new Package("EyeSoft.Data.Nhibernate"),
			//new Package("EyeSoft.Data.SqLite"),
			//new Package("EyeSoft.Domain"),
			//new Package("EyeSoft.FluentValidation"),
			//new Package("EyeSoft.ServiceLocator"),
			//new Package("EyeSoft.ServiceLocator.Windsor"),
			////new Package("EyeSoft.Web", "4.5"),
			//new Package("EyeSoft.Windows", "4.0"),
			//new Package("EyeSoft.Windows.Model", "4.0")
		};
		
		return packages;
	}
}

#region Base
public class NugetHelper
{
	private readonly string basePath;
	private readonly string solutionName;
	private readonly string publishFolder;

	public NugetHelper(string basePath, string solutionName, IEnumerable<Package> packages)
	{
		this.basePath = basePath;
		this.solutionName = solutionName;
		Packages = packages;
		publishFolder = PathHelper.Combine(basePath, @"..\Packages");
	}
	
	public string BasePath => basePath;

	public IEnumerable<Package> Packages { get; }

	public void ShowAllVersions()
	{
		new DirectoryInfo(basePath)
			.GetFiles("*.*", SearchOption.AllDirectories)
			.Where(x =>
				new[] { ".nuspec", ".csproj" }.Contains(x.Extension) &&
				!x.FullName.Contains("obj\\debug", StringComparison.InvariantCultureIgnoreCase))
			.Select(x => new
			{
				x.FullName,
				Lines = File.ReadAllLines(x.FullName).Where(y =>
					(y.Contains("<version>") ||
					y.Contains("<dependency ")) &&
					y.Contains("3."))
			})
			.Where(x => x.Lines.Any())
			.Dump();
	}

	public void BuildSolution()
	{
		var msBuildPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\msbuild.exe";
		var msbuild = $@"""{msBuildPath}"" ""{basePath}\{solutionName}"" /p:Configuration=""Release"" /t:Rebuild /p:IncludeSource=true /p:IncludeSymbols=true /p:GeneratePackageOnBuild=true /m";
		msbuild.Dump();
		Util.Cmd(msbuild);
	}

	public void BuildProject(Package package)
	{
		var msBuildPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\msbuild.exe";
		
		var projectFile = FindProject(package);

		var msbuild = $@"""{msBuildPath}"" ""{projectFile.FullName}"" /p:Configuration=""Release"" /t:Rebuild /p:IncludeSource=true /p:IncludeSymbols=true /p:GeneratePackageOnBuild=true /p:SymbolPackageFormat=snupkg";
		
		Util.Cmd(msbuild);
	}

	public void Pack(Package package)
	{
		var projectFile = FindProject(package);
		
		var nugetPathCommand = $"dotnet pack {projectFile.FullName} -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -c:Release";
		
		var nuspecFile = projectFile.Directory
			.GetFiles("*.nuspec")
			.SingleOrDefault(x => x.Exists);
		
		if (nuspecFile != null)
		{
			nugetPathCommand = $"{nugetPathCommand} /p:NuspecFile=Package.nuspec";
		}
		
		ConsoleHelper.WriteLine($"Building {package.Name}...", ConsoleColor.Blue);
		ConsoleHelper.WriteLine($"{nugetPathCommand}", ConsoleColor.Green);
		
		Util.Cmd(nugetPathCommand);
		
		Console.WriteLine();
	}

	public void BuildAndPackAndPushAll()
	{
		var projects =
			new DirectoryInfo(basePath)
				.GetFiles("*.csproj", SearchOption.AllDirectories)
				.Select(x => new { x.FullName, Package = FindPackage(x) })
				.Where(x => x.Package != null)
				.Select(x => new { x.Package, x.FullName });
				//.Where(x => x.Package.Name == "EyeSoft.Windows.Model");

		BuildSolution();
		
		foreach (var project in projects)
		{
			//BuildProject(project.Package);

			Pack(project.Package);
		}
		
		CopyAllPackagesToFolderForPublish(false);
		
		PushAllPackages();
	}

	public void PushAllPackages()
	{
		var packages =
			new DirectoryInfo(publishFolder)
				.GetFiles("*.*", SearchOption.AllDirectories)
				.Where(x =>
					(x.Name.EndsWith(".snupkg") ||
					x.Name.EndsWith(".symbols.nupkg") ||
					x.Name.EndsWith(".nupkg")))
				.ToArray();

		foreach (var package in packages)
		{
			try
			{
				ConsoleHelper.WriteLine($"Pushing {package.Name}...", ConsoleColor.Blue);
				var pushCommand = $"nuget push \"{package}\" -Source https://api.nuget.org/v3/index.json";

				Util.Cmd(pushCommand);
				
				Console.WriteLine();
			}catch{}
		}
	}

	public void CopyAllPackagesToFolderForPublish(bool deleteSymbols)
	{
		var nuget = new DirectoryInfo(basePath);

		var symbolsPackages =
			nuget
				.GetFiles("*.*", SearchOption.AllDirectories)
				.Where(x =>
					x.Directory.Name == "Release" &&
					x.Name.StartsWith("EyeSoft") &&
					(x.Name.EndsWith(".snupkg") ||
					x.Name.EndsWith(".symbols.nupkg") ||
					x.Name.EndsWith(".nupkg")))
				.ToArray()
				.Dump();

		if (deleteSymbols)
		{
			symbolsPackages.ToList().ForEach(x => x.Delete());
			return;
		}

		var builtSymbols =
			symbolsPackages
				.Where(x => x.Directory.Name == "Release")
				.Select(x => Explorer.CreateLink(x.FullName))
				.Dump();
		
		foreach (var buildPackage in symbolsPackages)
		{
			var destination = Path.Combine(publishFolder, buildPackage.Name);
			
			Console.WriteLine(destination);
			buildPackage.CopyTo(destination, true);
		}
	}

	public void ReplaceOldVersionWithNewVersion(string currentVersion, string newVersion, params string[] years)
	{
		var files =
		new DirectoryInfo(basePath)
			.GetFiles("*.*", SearchOption.AllDirectories)
			.Where(x => new[] { ".csproj" }.Contains(x.Extension) || x.Name == "AssemblyInfo.cs" || x.Name == "Package.nuspec")
			.Select(x => new { File = x, Text = File.ReadAllText(x.FullName, Encoding.UTF8) })
			.ToArray();

		if (years == null || !years.Any())
		{
			throw new InvalidOperationException("Years must be not null or empty.");
		}

		var currentYear = DateTime.UtcNow.Year;
		var list = new List<string>();

		foreach (var file in files)
		{
			var updatedText = file.Text;

			switch (file.File.Extension)
			{
				case ".cs":
					updatedText =
						updatedText
							.Replace($"[assembly: AssemblyVersion(\"{currentVersion}\")]", $"[assembly: AssemblyVersion(\"{newVersion}\")]")
							.Replace($"[assembly: AssemblyFileVersion(\"{currentVersion}\")", $"[assembly: AssemblyFileVersion(\"{newVersion}\")");

					foreach (var year in years)
					{
						updatedText = updatedText
							.Replace($"[assembly: AssemblyCopyright(\"Copyright © EyeSoft {year}\")]", $"[assembly: AssemblyCopyright(\"Copyright © EyeSoft {currentYear}\")]");
					}

					break;

				case ".csproj":
					updatedText = updatedText.Replace($"<Version>{currentVersion}</Version>", $"<Version>{newVersion}</Version>");

					foreach (var year in years)
					{
						updatedText = updatedText
							.Replace($"<Copyright>EyeSoft ©{year}</Copyright>", $"<Copyright>EyeSoft ©{currentYear}</Copyright>");
					}

					break;

				case ".nuspec":
					updatedText = updatedText.Replace($@"version=""{currentVersion}""", $@"version=""{newVersion}""");

					foreach (var year in years)
					{
						updatedText = updatedText
							.Replace($"<Copyright>EyeSoft ©{year}</Copyright>", $"<Copyright>EyeSoft ©{currentYear}</Copyright>");
					}

					break;
			}

			if (file.Text != updatedText)
			{
				list.Add(file.File.FullName);
				File.WriteAllText(file.File.FullName, updatedText, Encoding.UTF8);
			}
		}

		list.Dump();
	}

	public string GetNewVersion()
	{
		var dateTime = DateTime.UtcNow;
		var version = new Version(3, 0).Increment(dateTime);

		return $"{version}";
	}

	public FileInfo FindProject(Package package)
	{
		var projects = new DirectoryInfo(basePath)
			.GetFiles("*.csproj", SearchOption.AllDirectories)
			.Where(x => $"{package.Name}.csproj" == x.Name && (package.Framework != null ? x.Directory.Parent.Name == package.Framework : true));
			
		var project = projects.Single();

		return project;
	}
	
	public Package FindPackage(FileInfo file)
	{
		var package = Packages.SingleOrDefault(y => $"{y.Name}.csproj" == file.Name);
		
		return package;
	}
}

public class Package
{
	public Package(string name) : this(name, null)
	{
	}

	public Package(string name, string framework)
	{
		Name = name;
		Framework = framework;
	}

	public string Name { get; }
	public string Framework { get; }
}

public static class VersionHelper
{
	private static readonly DateTime referenceDate = new DateTime(2000, 1, 1);

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
		return new Version(major, minor, (int)buildAndRevision.Build, (int)buildAndRevision.Revision);
	}

	public static BuildAndRevision GenerateBuildAndRevision(DateTime? dateTime = null)
	{
		var localDateTime = dateTime ?? DateTime.Now;

		var build = (localDateTime.Date - referenceDate).TotalDays;
		var revision = (localDateTime - localDateTime.Date).TotalSeconds / 2;

		return new BuildAndRevision((int)build, (int)revision);
	}

	public static DateTime GetDateTime(this Version version)
	{
		var date = referenceDate.AddDays(version.Build).AddSeconds(version.Revision * 2);
		return date;
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

public static class Explorer
{
	public static Hyperlinq CreateLink(string path, string text = null)
	{
		var hyperlinq = new Hyperlinq(() => OpenFileInExplorer(path), text?? path);
		
		return hyperlinq;
	}
	
	public static void OpenFileInExplorer(string path)
	{
		Process.Start("explorer", string.Format("/select, \"{0}\"", path));
	}
}

public static class PathHelper
{
	public static string Combine(this string root, params string[] pathTokens)
	{
		var directoryInfo = new DirectoryInfo(root);

		if (pathTokens == null || !pathTokens.Any())
		{
			return root;
		}

		const string UpFolder = @"..\";

		var count = pathTokens[0].Split(new[] { UpFolder }, StringSplitOptions.None).Count() - 1;

		if (count > 0)
		{
			while (count > 0)
			{
				directoryInfo = directoryInfo.Parent;
				count--;
			}

			pathTokens[0] = pathTokens[0].Replace(UpFolder, null);
		}

		var allPath = new[] { directoryInfo.FullName }.Union(pathTokens).ToArray();

		return Path.Combine(allPath);
	}
}
#endregion

public static class ConsoleHelper
{
	public static void WriteLine()
	{
		Console.WriteLine();
	}

	public static void WriteLine(string s, ConsoleColor color = ConsoleColor.Black)
	{
		Console.WriteLine(Util.WithStyle(s, $"color:{color}"));
	}
}