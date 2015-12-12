namespace EyeSoft.Nuget.Publisher.Shell.Nuget
{
    using EyeSoft.Nuget.Publisher.Shell.LinqPad;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using EyeSoft.Nuget.Publisher.Shell.Diagnostics;

    using NuGet;

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

            ////TargetFramework = Version.Parse(targetFramework).ToString();
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
}