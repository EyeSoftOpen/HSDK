<Query Kind="Program" />

void Main()
{
	//new NugetPackageMetaHelper().DeleteAllPackages();
	
	var packages = new NugetPackageMetaHelper().GetPackages();
	
	new ProjectMetadataHelper().GetProjects(packages);
}

#region Nuget
public class NugetPackageMetaHelper
{
	public void DeleteAllPackages()
	{
		new DirectoryInfo(PathHelper.CurrentFolderPath())
			.GetFiles("*.nupkg", SearchOption.AllDirectories)
			.Where(x =>
				!x.FullName.Contains("Nuget.Packages") &&
				!x.FullName.Contains("packages") &&
				!x.FullName.Contains("Libraries") &&
				!x.Name.Contains(".symbols"))
			.ToList()
			.ForEach(x => x.Delete());
	}
	
	public IEnumerable<NugetPackage> GetPackages()
	{
		var packages =
			new DirectoryInfo(PathHelper.CurrentFolderPath())
				.GetFiles("*.nupkg", SearchOption.AllDirectories)
				.Where(x =>
					!x.FullName.Contains("Nuget.Packages") &&
					!x.FullName.Contains("packages") &&
					!x.FullName.Contains("Libraries") &&
					!x.Name.Contains(".symbols"))
				.Select(x => new { x.FullName, Name = Path.GetFileNameWithoutExtension(x.Name) })
				.Distinct()
				.Select(x => new { x.FullName, Name = x.Name, Version = GetVersion(x.Name) })
				.Select(x => new NugetPackage(x.FullName, x.Name.Replace(x.Version.ToString(), null).Trim('.'), x.Version))
				.ToArray();
				
		return packages;
	}

	private Version GetVersion(string name)
	{
		var version = name;

		for (var i = 0; i < name.Length; i++)
		{
			if (char.IsDigit(version[i]))
			{
				return Version.Parse(version.Substring(i));
			}
		}

		return Version.Parse(version);
	}
}

public class NugetPackage
{
	public NugetPackage(string path, string name, Version version)
	{
		Path = path;
		Name = name;
		Version = version;
	}

	public string Path { get; }
	public string Name { get; }
	public Version Version { get; }
	
	object ToDump()
	{
		return new
			{
				Name = new Hyperlinq(() => Explorer.OpenFileInExplorer(Path), Name),
				Version = Version.ToString()
			};
	}
}
#endregion

#region Project
public class ProjectMetadataHelper
{
	public void GetProjects(IEnumerable<NugetPackage> packages)
	{
		var projects =
		new[]
			{
				"EyeSoft.Core.csproj",
				"EyeSoft.Domain.csproj",
				"EyeSoft.Accounting.csproj",
				"EyeSoft.Accounting.Italian.csproj",
				"EyeSoft.Accounting.Italian.Istat.csproj",
				"EyeSoft.FluentValidation.csproj",
				"EyeSoft.Data.Nhibernate.csproj",
				"EyeSoft.ServiceLocator.csproj",
				"EyeSoft.ActiveCampaign.csproj",
				"EyeSoft.Windows.Model.csproj",
				"EyeSoft.Windows.csproj"
			};
		
		new DirectoryInfo(PathHelper.CurrentFolderPath())
			.GetFiles("*.csproj", SearchOption.AllDirectories)
			.Where(x => packages.Any(p => p.Name == Path.GetFileNameWithoutExtension(x.Name)))
			.Select(x => Parse(x.FullName).Merge())
			.OrderBy(x => x.Package)
			.Select(x => new
				{
					x.Version,
					x.Authors,
					x.Product,					
					x.Company,
					x.Copyright,
					Url = x.ProjectUrl == null ? null : new Hyperlinq(x.ProjectUrl),
					Name = new Hyperlinq($"https://www.nuget.org/packages/{x.Package}", x.Package),
					x.Description,
					x.TargetFramework,
					Files = packages.Where(p => p.Name == x.Package)
				})			
			.Dump();
	}
	
	public ProjectMergedMetadata Parse(string projectPath)
	{
		var assemblyInfoPath = Path.Combine(new FileInfo(projectPath).DirectoryName, "Properties", "AssemblyInfo.cs");
	
		AssemblyInfoProjectMetadata	assemblyInfoProjectMetadata = null;
		
		if (File.Exists(assemblyInfoPath))
		{
			var parser = new AssemblyInfoProjectMetadataParser();

			assemblyInfoProjectMetadata = parser.Parse(assemblyInfoPath);
		}
		
		var projectMetadata = new XmlProjectMetadataParser().Parse(projectPath);
		
		return new ProjectMergedMetadata(Path.GetFileNameWithoutExtension(projectPath), projectMetadata, assemblyInfoProjectMetadata);
	}
}

public class PackageMetadata : ProjectMetadata
{
	public PackageMetadata(
		string package,
		string authors,
		string company,
		string product,
		string copyright,
		string projectUrl,
		string version,
		string description,
		string targetFramework)
		: base(package, authors, company, product, copyright, projectUrl, version, description, targetFramework)
	{
	}
}

public class ProjectMetadata
{
	public ProjectMetadata(
		string package,
		string authors,
		string company,
		string product,
		string copyright,
		string projectUrl,
		string version,
		string description,
		string targetFramework)
	{
		Version = version;
		Authors = authors;
		Company = company;
		Product = product;
		Copyright = copyright;
		ProjectUrl = projectUrl;
		Package = package;
		Description = description;
		TargetFramework = targetFramework;
	}

	public string Version { get; }
	public string ProjectUrl { get; }
	public string Authors { get; }
	public string Company { get; }
	public string Copyright { get; }
	public string Product { get; }
	public string Package { get; }
	public string Description { get; }
	public string TargetFramework { get; }
}

public class ProjectMergedMetadata
{
	public ProjectMergedMetadata(string project, XmlProjectMetadata xmlProject, AssemblyInfoProjectMetadata assemblyInfo)
	{
		Project = project;
		XmlProject = xmlProject;
		AssemblyInfo = assemblyInfo;
	}
	
	public string Project { get; }
	
	public XmlProjectMetadata XmlProject { get; }
	
	public AssemblyInfoProjectMetadata AssemblyInfo { get; }
	
	public ProjectMetadata Merge()
	{		
		return new ProjectMetadata
			(
				Project,				
				XmlProject.Authors,
				XmlProject.Company,
				XmlProject.Product,
				AssemblyInfo?.Copyright ?? XmlProject.Copyright,
				XmlProject.ProjectUrl,
				AssemblyInfo?.Version ?? XmlProject.Version,
				AssemblyInfo?.Description ?? XmlProject.Description,
				XmlProject.TargetFramework				
			);
	}
}
#endregion

#region AssembyInfo parse
public class AssemblyInfoProjectMetadataParser
{
	public AssemblyInfoProjectMetadata Parse(string projectPath)
	{
		var metaTags = new[] { "Company", "Copyright", "Title", "Description", "AssemblyVersion" };

		var lines = File.ReadAllLines(projectPath, Encoding.UTF8);
	
		var meta =
			metaTags
				.Select(x => Parse(x, lines.Single(l => l.Contains(x))))
				.ToDictionary(k => k.Key, v => v.Value);

		var company = meta["Company"];
		var copyright = meta["Copyright"];
		var title = meta["Title"];
		var description = meta["Description"];
		var assemblyVersion = meta["AssemblyVersion"].Replace("Version(", null);
		
		return new AssemblyInfoProjectMetadata(company, copyright, title, description, assemblyVersion);
	}
	
	public KeyValuePair<string, string> Parse(string meta, string line)
	{
		var value = line
			.Replace("[assembly: Assembly", null)
			.Replace(meta, null)
			.Replace("\"", null)
			.Trim('(', ']', ')', ' ');
		
		value = string.IsNullOrWhiteSpace(value) ? null : value;
		
		return KeyValuePair.Create(meta, value);
	}
}

public class AssemblyInfoProjectMetadata
{
	public AssemblyInfoProjectMetadata(
		string company,
		string copyright,
		string title,
		string description,
		string version)
	{
		Company = company;
		Copyright = copyright;
		Title = title;
		Description = description;
		Version = version;
	}

	public string Company { get; }
	public string Copyright { get; }
	public string Title { get; }
	public string Description { get; }
	public string Version { get; }
}
#endregion

#region XML project parse
public class XmlProjectMetadataParser
{
	private XDocument document;
	
	public XmlProjectMetadata Parse(string projectPath)
	{
		document = XDocument.Load(projectPath);

		var isNewFormat = document.Root.Attributes().Any(x => x.Name == "Sdk");
		
		if (projectPath.Contains("EyeSoft.Core"))
		{
			//document.Dump();
		}
		
		var description = isNewFormat ? GetProperty("Description", isNewFormat) : null;
		var authors = GetProperty("Authors", isNewFormat);
		var company = GetProperty("Company", isNewFormat);
		var copyright = GetProperty("Copyright", isNewFormat);
		var product = GetProperty("Product", isNewFormat);
		var projectUrl = GetProperty("PackageProjectUrl", isNewFormat);
		
		var targetFramework = GetProperty(isNewFormat ? "TargetFramework" : "TargetFrameworkVersion", isNewFormat);
		var version = GetProperty("Version", isNewFormat);
		var signAssembly = GetProperty("SignAssembly", isNewFormat);
		var assemblyOriginatorKeyFile = GetProperty("AssemblyOriginatorKeyFile", isNewFormat);
		
		var metadata = new XmlProjectMetadata(
			description,
			authors,
			company,
			product,
			copyright,
			projectUrl,
			targetFramework,
			version,
			signAssembly,
			assemblyOriginatorKeyFile);
			
		return metadata;
	}

	private string GetProperty(string meta, bool isNewFormat)
	{
		if (!isNewFormat)
		{
			var xmlns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");
			
			var value =
				document
					.Descendants(xmlns + "PropertyGroup")
					.Elements(xmlns + meta)
					.SingleOrDefault()?
					.Value;
					
			return value;
		}
		else
		{
			var value = document.Descendants(meta).SingleOrDefault()?.Value;

			return value;
		}
	}
}

public class XmlProjectMetadata
{
	public XmlProjectMetadata(
		string description,
		string authors,
		string company,
		string product,
		string copyright,
		string projectUrl,
		string targetFramework,
		string version,
		string signAssembly,
		string assemblyOriginatorKeyFile)
	{
		Description = description;
		Authors = authors;
		Company = company;
		Product = product;
		Copyright = copyright;
		ProjectUrl = projectUrl;
		TargetFramework = targetFramework;
		Version = version;
		SignAssembly = signAssembly;
		AssemblyOriginatorKeyFile = assemblyOriginatorKeyFile;
	}

	public string Description { get; }
	public string Authors { get; }
	public string Company { get; }
	public string Product { get; }
	public string Copyright { get; }	
	public string ProjectUrl { get; }	
	public string TargetFramework { get; }
	public string Version { get; }
	public string SignAssembly { get; }
	public string AssemblyOriginatorKeyFile { get; }
}
#endregion