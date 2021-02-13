<Query Kind="Program" />

void Main()
{
	//Push pack
	//D:\GitHub\HSDK\Main\.nuget\nuget push EyeSoft.Windows.Model.3.0.7713.28810.nupkg -Source https://www.nuget.org/api/v2/package
	
	var packages = new[]
		{
			"EyeSoft.Core",
			"EyeSoft.Domain",
			"EyeSoft.FluentValidation",
			"EyeSoft.Windows",
			"EyeSoft.Windows.Model"
		}
		.Select(x => $"{x}.csproj");
	
	new DirectoryInfo(@"D:\GitHub\HSDK\Main")
		.GetFiles("*.csproj", SearchOption.AllDirectories)
		.Where(x => packages.Contains(x.Name))
		.Select(x => x.FullName)
		.AsParallel()
		.Select(x => 		
			{
				
				Pack(x, "3.0.7714.20388");
				//return;
				var nupkgPath = GetNugetPackagePath(x);
				
				Publish(nupkgPath);
				
				return nupkgPath;
			})
		.Dump();
		
}

string GetNugetPackagePath(string projectPath)
{
	var releasePath = Path.Combine(new FileInfo(projectPath).DirectoryName, "bin", "Release");
	
	var nugetPkg = GetNugetPackagePath(projectPath, releasePath);
	
	if (nugetPkg == null)
	{
		nugetPkg = GetNugetPackagePath(projectPath, new FileInfo(projectPath).DirectoryName);
	}
	
	return nugetPkg;
}

string GetNugetPackagePath(string projectPath, string folderPath)
{
	var nugetPkg = new DirectoryInfo(folderPath)
		.GetFiles("*.nupkg")
		.Where(x => !x.Name.EndsWith(".symbols.nupkg"))
		.SingleOrDefault()?.FullName;

	return nugetPkg;
}

public void Pack(string projectPath, string version)
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

	var command = $@"D:\GitHub\HSDK\Main\.nuget\nuget.exe pack {path} -Prop Configuration=Release -Symbols -SymbolPackageFormat snupkg";

	//if (isNuspec)
	//{
	//	var id = Path.GetFileNameWithoutExtension(projectPath);
	//	command = $"{command} -Version {version} -Id {id}";
	//}

	command.Dump();
	Util.Cmd(command); 
}

public void Publish(string nupkgPath)
{
	Util.Cmd(@$"D:\GitHub\HSDK\Main\.nuget\nuget.exe push ""{nupkgPath}"" -Source https://www.nuget.org/api/v2/package");
}