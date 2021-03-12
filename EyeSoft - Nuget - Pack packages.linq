<Query Kind="Program">
  <DisableMyExtensions>true</DisableMyExtensions>
</Query>

#load ".\EyeSoft - Nuget - Update all projects version to publish"

void Main()
{
	var nugetHelper = new HsdkNugetHelper();
	
	var projects =
		new DirectoryInfo(nugetHelper.BasePath)
			.GetFiles("*.csproj", SearchOption.AllDirectories)
			.Select(x => new { x.FullName, Package = nugetHelper.FindPackage(x) })
			.Where(x => x.Package != null)
			.Select(x => new { x.Package, x.FullName })
			.Take(1);
			
	foreach (var project in projects)
	{
		nugetHelper.BuildProject(project.Package);
		
		nugetHelper.Pack(project.Package);
	}
}

