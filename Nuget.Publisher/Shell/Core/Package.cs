namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Text;

	using EyeSoft.Nuget.Publisher.Shell.LinqPad;

	using NuGet;

	public class Package
	{
		private readonly FileInfo assemblyInfoFile;

		private readonly string[] lines;

		private readonly FileInfo nuspecFile;

		public Package(FileInfo assemblyInfoFile, string[] lines, string title, string version, string targetFramework, FileInfo nuspecFile)
			: this(assemblyInfoFile, lines, title, version, targetFramework)
		{
			this.nuspecFile = nuspecFile;
			HasNuget = true;
		}

		public Package(FileInfo assemblyInfoFile, string[] lines, string title, string version, string targetFramework)
		{
			this.assemblyInfoFile = assemblyInfoFile;
			this.lines = lines;

			Title = title;
			PackageVersion = Version.Parse(version).ToString();
			TargetFramework = new Hyperlinq(() => Process.Start(assemblyInfoFile.Directory.Parent.FullName), targetFramework);
			////TargetFramework = Version.Parse(targetFramework).ToString();
		}

		public string Title { get; private set; }

		public string PackageVersion { get; private set; }

		public Hyperlinq TargetFramework { get; private set; }

		public bool HasNuget { get; private set; }

		public IEnumerable<PackageUpdate> TryUpdateNuspecDependencies(IReadOnlyDictionary<string, Func<Version>> packages)
		{
			var nuspec = File.ReadAllText(nuspecFile.FullName);

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

					var hasDepenciesUpdated = dependenciesToUpdate.Any();

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

					Storage.WriteAllText(nuspecFile.FullName, nuspec);

					return updates;
				}
			}
		}

		public void IncrementAssemblyInfo(string newVersion)
		{
			var assemblyInfoLines =
				lines
					.Select((line, index) => new AssemblyInfoLine(index, line))
					.ToArray();

			UpdateAssemblyVersionLine(newVersion, assemblyInfoLines, AssemblyInfoData.AssemblyVersion);
			UpdateAssemblyVersionLine(newVersion, assemblyInfoLines, AssemblyInfoData.AssemblyFileVersion);

			Storage.WriteAllLines(assemblyInfoFile.FullName, assemblyInfoLines.Select(x => x.Line).ToArray());
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", Title, PackageVersion, TargetFramework);
		}

		private static void UpdateAssemblyVersionLine(
			string newVersion,
			IReadOnlyList<AssemblyInfoLine> assemblyInfoLines,
			AssemblyInfoData assemblyInfo)
		{
			var assemblyInfoVersion = assemblyInfo.ToString();

			var assemblyVersion = assemblyInfoLines.Single(x => x.Line.Contains(assemblyInfoVersion));

			var assemblyVersionLine = assemblyVersion.Line;

			var version = AssemblyInfo.GetData(assemblyVersion.Line, assemblyInfo);

			assemblyInfoLines[assemblyVersion.Index].Line = assemblyVersionLine.Replace(version, newVersion);
		}
	}
}