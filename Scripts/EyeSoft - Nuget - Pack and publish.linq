<Query Kind="Program" />

void Main()
{
	//Push pack
	//D:\GitHub\HSDK\Main\.nuget\nuget push EyeSoft.Windows.Model.3.0.7713.28810.nupkg -Source https://www.nuget.org/api/v2/package
	
	//D:\GitHub\HSDK\Packages

	new[]
	{
		//"EyeSoft.Core",
		//"EyeSoft.Domain",
		//"EyeSoft.FluentValidation",
		//"EyeSoft.Windows",
		"EyeSoft.Windows.Model"
	}
	.Pack(@"D:\GitHub\HSDK\Main", "3.0.7714.24435");
}

public static class NugetHelper
{
	public static void Pack(this IEnumerable<string> packages, string basePath, string version)
	{
		var projects = packages.Select(x => $"{x}.csproj").ToArray();

		new DirectoryInfo(basePath)
			.GetFiles("*.csproj", SearchOption.AllDirectories)
			.Where(x => projects.Contains(x.Name))
			.Select(x => x.FullName)
			.AsParallel()
			.Select(x =>
				{
					Pack(x, version);
	
					var nupkgPath = GetNugetPackagePath(x);

					Publish(nupkgPath);

					return nupkgPath;
				})
			.Dump();
	}

	private static string GetNugetPackagePath(string projectPath)
	{
		var releasePath = Path.Combine(new FileInfo(projectPath).DirectoryName, "bin", "Release");

		var nugetPkg = GetNugetPackagePath(projectPath, releasePath);

		if (nugetPkg == null)
		{
			nugetPkg = GetNugetPackagePath(projectPath, new FileInfo(projectPath).DirectoryName);
		}

		return nugetPkg;
	}

	private static string GetNugetPackagePath(string projectPath, string folderPath)
	{
		var nugetPkg = new DirectoryInfo(folderPath)
			.GetFiles("*.nupkg")
			.Where(x => !x.Name.EndsWith(".symbols.nupkg"))
			.SingleOrDefault()?.FullName;

		return nugetPkg;
	}

	public static void Pack(string projectPath, string version)
	{
		//var command = $"dotnet pack --include-symbols --include-source --configuration Release \"{projectPath}\"";

		var projectDirectory = new FileInfo(projectPath).Directory;

		var path = projectPath;

		//var nuspecPath = projectDirectory.GetFiles("*.nuspec").SingleOrDefault();
		//
		//var isNuspec = nuspecPath != null;
		//
		//var path = nuspecPath?.FullName;
		//
		//path = path?? projectPath;

		//var command = $@"D:\GitHub\HSDK\Main\.nuget\nuget.exe pack {path} -Prop Configuration=Release -Symbols -SymbolPackageFormat snupkg -Version {version}";
		var command = $@"D:\GitHub\HSDK\Main\.nuget\nuget.exe pack {path} -Prop Configuration=Release -Symbols -SymbolPackageFormat snupkg";

		//if (isNuspec)
		//{
		//	var id = Path.GetFileNameWithoutExtension(projectPath);
		//	command = $"{command} -Version {version} -Id {id}";
		//}

		command.Dump();
		Util.Cmd(command);
	}

	public static void Publish(string nupkgPath)
	{
		Util.Cmd(@$"D:\GitHub\HSDK\Main\.nuget\nuget.exe push ""{nupkgPath}"" -Source https://www.nuget.org/api/v2/package");
	}
}