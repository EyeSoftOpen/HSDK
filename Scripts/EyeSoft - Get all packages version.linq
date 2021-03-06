<Query Kind="Program">
  <Reference Relative="Nuget.Publisher\Shell\bin\Debug\EyeSoft.Nuget.Publisher.Core.dll">D:\Pw.Vs.Com\Dc\Es.Hsdk\Nuget.Publisher\Shell\bin\Debug\EyeSoft.Nuget.Publisher.Core.dll</Reference>
  <Namespace>EyeSoft.Nuget.Publisher.Core.Core</Namespace>
  <Namespace>EyeSoft.Nuget.Publisher.Core.Workflow</Namespace>
</Query>

void Main()
{
	var currentFolder = Path.GetDirectoryName(Util.CurrentQueryPath);
	
	var packagesVersion =
		new DirectoryInfo(currentFolder)
			.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories)
			.Select(x => File.ReadAllLines(x.FullName, Encoding.Default))
			.Select(x => new { Title = AssemblyInfo.GetData(x, AssemblyInfoData.AssemblyTitle), Version = AssemblyInfo.GetData(x, AssemblyInfoData.AssemblyVersion) })
			.Where(x => HsdkWorkflow.PackagesId.Contains(x.Title))
			.GroupBy(x => x.Title)
			.Select(x => new { Title = x.Key, x.First().Version })
			.OrderBy(x => x.Title)
			.GroupBy(x => x.Version)
			.Select(x => new { Version = x.Key, Packages = x.Select(t => t.Title) });
		
	packagesVersion.Dump($"Packages - Latest version {packagesVersion.Max(x => Version.Parse(x.Version))}");
	
	packagesVersion
		.Select(x => Version.Parse(x.Version).ToString())
		.Distinct()
		.Dump("All versions found");
}