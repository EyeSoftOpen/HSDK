<Query Kind="Program" />

void Main()
{
	//new NugetPackageMetaHelper().DeleteAllPackages();
	
	var basePath = PathHelper.CurrentFolderPath();
	
	var packages = new NugetPackageMetaHelper(basePath).GetPackages().Dump("Found packages with symbols");
	
	var foundPackages = new ProjectMetadataHelper(basePath).GetPackages(packages).Dump("Packages metadata");
	
	new ProjectMetadataHelper(basePath).CopyPackages(foundPackages).Dump("Copied packages to be published and placed under source control");
}

#region Nuget
public class NugetPackageMetaHelper
{
	private readonly string basePath;
	
	public NugetPackageMetaHelper(string basePath)
	{
		this.basePath = basePath;
	}
	
	public void DeleteAllPackages()
	{
		new DirectoryInfo(basePath)
			.GetFiles("*.nupkg", SearchOption.AllDirectories)
			.Where(x =>
				!x.FullName.Contains("Nuget.Packages") &&
				!x.FullName.Contains("packages") &&
				!x.FullName.Contains("Libraries") &&
				!x.Name.Contains(".symbols"))
			.Select(x => x.FullName)
			.Dump("Deleted packages")
			.ToList()
			.ForEach(x => File.Delete(x));
	}
	
	public IEnumerable<NugetPackage> GetPackages()
	{
		var packages =
			new DirectoryInfo(basePath)
				.GetFiles("*.symbols.nupkg", SearchOption.AllDirectories)
				.Where(x =>
					!x.FullName.Contains("Nuget.Packages") &&
					!x.FullName.Contains("packages") &&
					!x.FullName.Contains("Libraries") &&
					x.Directory.Name == "Release")
				.Select(x => new { x.FullName, Name = Path.GetFileNameWithoutExtension(x.Name) })
				.Distinct()
				.Select(x => new { x.FullName, Name = x.Name, Version = GetVersion(x.Name) })
				.Select(x => new NugetPackage(x.FullName, x.Name.Replace(".symbols", null).Replace(x.Version.ToString(), null).Trim('.'), x.Version))
				.OrderBy(x => x.Name)
				.ToArray();
				
		return packages;
	}

	private Version GetVersion(string name)
	{
		var version = name.Replace(".symbols", null);

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
	private readonly string basePath;

	public ProjectMetadataHelper(string basePath)
	{
		this.basePath = basePath;
	}

	public IEnumerable<NugetPackageToPublish> GetPackages(IEnumerable<NugetPackage> foundPackages)
	{
		var packages =
			new DirectoryInfo(basePath)
				.GetFiles("*.csproj", SearchOption.AllDirectories)
				.Where(x => foundPackages.Any(p => p.Name == Path.GetFileNameWithoutExtension(x.Name)))
				.Select(x => Parse(x.FullName).Merge())
				.OrderBy(x => x.Package)
				.Select(x =>
					new NugetPackageToPublish(
						x.Version,
						x.Authors,
						x.Product,					
						x.Company,
						x.Copyright,
						x.ProjectUrl == null ? null : new Hyperlinq(x.ProjectUrl),
						new Hyperlinq($"https://www.nuget.org/packages/{x.Package}", x.Package),
						x.Description,
						x.Tags,
						x.TargetFramework,
						foundPackages.Where(p => p.Name == x.Package)
					))
					.ToArray();
					
		return packages;
	}

	public IEnumerable<NugetPackageToPublish> CopyPackages(IEnumerable<NugetPackageToPublish> foundPackages)
	{
		var copied =  new List<NugetPackageToPublish>();
		
		var nugetPackagesFolder = Path.Combine(basePath, "Nuget.Packages");
		
		foreach(var package in foundPackages)
		{
			var newerPackage = package.Packages.OrderByDescending(x => x.Version).First();
			
			var packageFileName = Path.GetFileName(newerPackage.Path);
			
			var nugetPackagePath = Path.Combine(nugetPackagesFolder, packageFileName);
			
			if (!File.Exists(nugetPackagePath))
			{
				File.Copy(newerPackage.Path, nugetPackagePath);
				
				copied.Add(package);
			}
		}
		
		return copied;
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
		
		var project = new XmlProjectMetadataParser().Parse(projectPath);
		
		return new ProjectMergedMetadata(Path.GetFileNameWithoutExtension(projectPath), project, assemblyInfoProjectMetadata);
	}
}

public class NugetPackageToPublish
{
	public NugetPackageToPublish(
		string version,
		string authors,
		string product,
		string company,
		string copyright,
		Hyperlinq projectUrl,
		Hyperlinq nugetUrl,
		string description,
		string tags,
		string targetFramework,
		IEnumerable<NugetPackage> packages)
	{
		Version = version;
		Authors = authors;
		Product = product;
		Company = company;
		Copyright = copyright;
		ProjectUrl = projectUrl;
		NugetUrl = nugetUrl;
		Description = description;
		Tags = tags;
		TargetFramework = targetFramework;
		Packages = packages;
	}

	public string Version { get; }
	public string Authors { get; }
	public string Product { get; }
	public string Company { get; }
	public string Copyright { get; }
	public Hyperlinq ProjectUrl { get; }
	public object NugetUrl { get; }
	public string Description { get; }
	public string Tags { get; }
	public string TargetFramework { get; }
	public IEnumerable<NugetPackage> Packages { get; }
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
		string tags,
		string targetFramework)
		: base(package, authors, company, product, copyright, projectUrl, version, description, tags, targetFramework)
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
		string tags,
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
		Tags = tags;
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
	public string Tags { get; }
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
				XmlProject.Tags,
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
	public XmlProjectMetadata Parse(string projectPath)
	{
		var document = XDocument.Load(projectPath);

		var isNewFormat = document.Root.Attributes().Any(x => x.Name == "Sdk");
		
		if (projectPath.Contains("EyeSoft.Core"))
		{
			//document.Dump();
		}
		
		var description = isNewFormat ? GetProperty(document, "Description", isNewFormat) : null;
		var tags = GetProperty(document, "PackageTags", isNewFormat);
		var authors = GetProperty(document, "Authors", isNewFormat);
		var company = GetProperty(document, "Company", isNewFormat);
		var copyright = GetProperty(document, "Copyright", isNewFormat);
		var product = GetProperty(document, "Product", isNewFormat);
		var projectUrl = GetProperty(document, "PackageProjectUrl", isNewFormat);
		
		var targetFramework = GetProperty(document, isNewFormat ? "TargetFramework" : "TargetFrameworkVersion", isNewFormat);
		var version = GetProperty(document, "Version", isNewFormat);
		var signAssembly = GetProperty(document, "SignAssembly", isNewFormat);
		var assemblyOriginatorKeyFile = GetProperty(document, "AssemblyOriginatorKeyFile", isNewFormat);
		
		var metadata = new XmlProjectMetadata(
			description,
			tags,
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

	private string GetProperty(XDocument document, string meta, bool isNewFormat)
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
		string tags,
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
		Tags = tags;
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
	public string Tags { get; }
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