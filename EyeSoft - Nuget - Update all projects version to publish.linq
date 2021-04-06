<Query Kind="Program">
  <DisableMyExtensions>true</DisableMyExtensions>
</Query>

void Main()
{
	Util.AutoScrollResults = true;
	
	//ICommandLine commandLine = new NullCommandLine();	
	ICommandLine commandLine = new CommandLine();	
	var nugetHelper = new HsdkNugetHelper(commandLine);
	
	//var newVersion = nugetHelper.GetNewVersion().Dump("New version");
	
	nugetHelper.BuildAndPackAndPushAll(false, true);
	
	//nugetHelper.CopyAllPackagesToFolderForPublish(false);
	//nugetHelper.PushAllPackages();
	//nugetHelper.ShowAllVersions();
	
	//return;
	//nugetHelper.ReplaceOldVersionWithNewVersion(
	//	new[]
	//	{
	//		"3.0.6836.35031",
	//		"3.0.7333.34942",
	//		"3.0.7349.34482",
	//		"3.0.7713.28810",
	//		"3.0.7714.20388",
	//		"3.0.7736.24658"
	//	}, "2020");

	//nugetHelper.BuildProject("EyeSoft.Core");
	//nugetHelper.CopyAllPackagesToFolderForPublish(false);
}

#region HSDK
#endregion
public class HsdkNugetHelper : NugetHelper
{
	public HsdkNugetHelper(ICommandLine commandLine) : base(commandLine, @"D:\GitHub\HSDK\Main", "EyeSoft.Hsdk.sln", GetPackages())
	{
		
	}

	private static IEnumerable<Package> GetPackages()
	{
		var packages = new[]
		{
			new Package("EyeSoft.Accounting"),
			new Package("EyeSoft.Accounting.Italian"),
			new Package("EyeSoft.Accounting.Italian.Istat"),
			new Package("EyeSoft.ActiveCampaign", "4.5"),
			new Package("EyeSoft.AutoMapper"),
			new Package("EyeSoft.Core", "4.0"),
			new Package("EyeSoft.Data"),
			new Package("EyeSoft.Data.Nhibernate"),
			new Package("EyeSoft.Data.SqLite"),
			new Package("EyeSoft.Domain"),
			new Package("EyeSoft.FluentValidation"),
			new Package("EyeSoft.ServiceLocator"),
			new Package("EyeSoft.ServiceLocator.Windsor"),
			//new Package("EyeSoft.Web", "4.5"),
			new Package("EyeSoft.Windows", "4.0"),
			new Package("EyeSoft.Windows.Model", "4.0")
		};
		
		return packages;
	}
}

#region Base
public class CommandLine : ICommandLine
{
	public void Execute(string commandText)
	{
		Util.Cmd(commandText);
	}
}

public class NullCommandLine : ICommandLine
{
	public void Execute(string commandText)
	{
		Console.WriteLine(commandText);
	}
}

public interface ICommandLine
{
	void Execute(string commandText);
}

public class NugetHelper
{
	private ICommandLine commandLine;
	private readonly string basePath;
	private readonly string solutionName;
	private readonly string publishFolder;

	public NugetHelper(ICommandLine commandLine, string basePath, string solutionName, IEnumerable<Package> packages)
	{
		this.commandLine = commandLine;
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
				!x.Name.Contains("Test") &&
				(x.Name.Equals("AssemblyInfo.cs", StringComparison.InvariantCultureIgnoreCase) ||
				new[] { ".csproj", ".nuspec" }.Contains(x.Extension)) &&
				(!x.FullName.Contains("obj\\debug", StringComparison.InvariantCultureIgnoreCase) &&
				!x.FullName.Contains("obj\\release", StringComparison.InvariantCultureIgnoreCase)))
			.Select(x => new
			{
				x.FullName,
				Lines = File.ReadAllLines(x.FullName).Where(y =>
					(
					y.Contains("<Version>") ||
					y.Contains("<version>") ||
					y.Contains("<dependency ")) &&
					y.Contains("3."))
			})
			.Where(x => x.Lines.Any())
			.Dump();
	}

	public void BuildSolution()
	{
		CleanSolution();
		
		var msBuildPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\msbuild.exe";
		var msbuild = $@"""{msBuildPath}"" ""{basePath}\{solutionName}"" /p:Configuration=""Release"" /t:Rebuild /p:IncludeSource=true /p:IncludeSymbols=true /p:GeneratePackageOnBuild=true /m";
		msbuild.Dump();
		commandLine.Execute(msbuild);
	}

	public void CleanSolution()
	{
		new DirectoryInfo(basePath)
			.GetDirectories("*.*", SearchOption.AllDirectories)
			.Where(x => x.FullName.IndexOf(@"\bin", StringComparison.InvariantCultureIgnoreCase) > 0)
			.ToList()
			.ForEach(x => { try { x.Delete(true); } catch { }});

		new DirectoryInfo(basePath)
			.GetDirectories("*.*", SearchOption.AllDirectories)
			.Where(x => x.FullName.IndexOf(@"\obj", StringComparison.InvariantCultureIgnoreCase) > 0)
			.ToList()
			.ForEach(x => { try { x.Delete(true); } catch { } });
	}

	public void BuildProject(Package package)
	{
		var msBuildPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Preview\MSBuild\Current\Bin\msbuild.exe";
		
		var projectFile = FindProject(package);

		var msbuild = $@"""{msBuildPath}"" ""{projectFile.FullName}"" /p:Configuration=""Release"" /t:Rebuild /p:IncludeSource=true /p:IncludeSymbols=true /p:GeneratePackageOnBuild=true /p:SymbolPackageFormat=snupkg";
		
		commandLine.Execute(msbuild);
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
		
		commandLine.Execute(nugetPathCommand);
		
		Console.WriteLine();
	}

	public void BuildAndPackAndPushAll(bool skipBuild = false, bool skipPack = false)
	{
		var projects =
			new DirectoryInfo(basePath)
				.GetFiles("*.csproj", SearchOption.AllDirectories)
				.Select(x => new { x.FullName, Package = FindPackage(x) })
				.Where(x => x.Package != null)
				.Select(x => new { x.Package, x.FullName });
		//.Where(x => x.Package.Name == "EyeSoft.Windows.Model");

		if (!skipBuild)
		{
			BuildSolution();
		}

		if (!skipPack)
		{
			foreach (var project in projects)
			{
				//BuildProject(project.Package);

				Pack(project.Package);
			}
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
					IsPackage(x.Name) &&
					(x.Name.EndsWith(".snupkg") ||
					x.Name.EndsWith(".symbols.nupkg") ||
					x.Name.EndsWith(".nupkg")))
				.ToArray();

		var apiKey = Util.GetPassword("Nuget.ApiKey");
		
		foreach (var package in packages)
		{
			try
			{
				ConsoleHelper.WriteLine($"Pushing {package.Name}...", ConsoleColor.Blue);
				var nugetPath = Path.Combine(basePath, ".nuget", "nuget.exe");

				var pushCommand = $"{nugetPath} push \"{package}\" -Source https://api.nuget.org/v3/index.json -ApiKey {apiKey}";

				commandLine.Execute(pushCommand);
				
				Console.WriteLine();
			}
			catch (Exception exception)
			{
				exception.Dump($"Error publishing {package.Name}");
			}
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
				.ToArray();

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
			
			buildPackage.CopyTo(destination, true);
		}
	}

	public void ReplaceOldVersionWithNewVersion(IEnumerable<string> previousVersions, string newVersion, params string[] years)
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

		foreach (var previousVersion in previousVersions)
		{
			foreach (var file in files)
			{
				var updatedText = file.Text;

				switch (file.File.Extension)
				{
					case ".cs":
						updatedText =
							updatedText
								.Replace($"[assembly: AssemblyVersion(\"{previousVersion}\")]", $"[assembly: AssemblyVersion(\"{newVersion}\")]")
								.Replace($"[assembly: AssemblyFileVersion(\"{previousVersion}\")", $"[assembly: AssemblyFileVersion(\"{newVersion}\")");

						foreach (var year in years)
						{
							updatedText = updatedText
								.Replace($"[assembly: AssemblyCopyright(\"Copyright © EyeSoft {year}\")]", $"[assembly: AssemblyCopyright(\"Copyright © EyeSoft {currentYear}\")]");
						}

						break;

					case ".csproj":
						updatedText = updatedText.Replace($"<Version>{previousVersion}</Version>", $"<Version>{newVersion}</Version>");

						foreach (var year in years)
						{
							updatedText = updatedText
								.Replace($"<Copyright>EyeSoft ©{year}</Copyright>", $"<Copyright>EyeSoft ©{currentYear}</Copyright>");
						}

						break;

					case ".nuspec":
						updatedText = updatedText.Replace($@"version=""{previousVersion}""", $@"version=""{newVersion}""");

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
		}

		list.Dump();
	}

	public string GetNewVersion()
	{
		var dateTime = DateTime.UtcNow;
		var version = new Version(3, 0).Increment(dateTime);

		return $"{version}";
	}

	private FileInfo FindProject(Package package)
	{
		var projects = new DirectoryInfo(basePath)
			.GetFiles("*.csproj", SearchOption.AllDirectories)
			.Where(x => $"{package.Name}.csproj" == x.Name && (package.Framework != null ? x.Directory.Parent.Name == package.Framework : true));
			
		var project = projects.Single();

		return project;
	}
	
	private Package FindPackage(FileInfo file)
	{
		var package = Packages.SingleOrDefault(y => $"{y.Name}.csproj" == file.Name);
		
		return package;
	}

	private bool IsPackage(string name)
	{
		var packageName = Path.GetFileNameWithoutExtension(name);
		
		var firstNumber =
			packageName
				.Select((x, index) => new { Char = x, Index = index })
				.First(x => char.IsNumber(x.Char))
				.Index;
				
		packageName = packageName.Substring(0, firstNumber - 1);
		
		var isPackage = Packages.Any(x => x.Name == packageName);

		return isPackage;
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