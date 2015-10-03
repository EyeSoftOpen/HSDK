namespace EyeSoft.Nuget.Publisher.Shell
{
    using Build;
    using EyeSoft.Nuget.Publisher.Shell.Nuget;

    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;

    public class DumpUpdatedPackagesStep : FluentWorkflowStep
    {
        private readonly BuildAndRevision buildAndRevision;

        private readonly NugetPackageResultCollection nugetPackageResultCollection;

        private SolutionSystemInfo solutionSystemInfo;

        public DumpUpdatedPackagesStep(SolutionSystemInfo solutionSystemInfo, NugetPackageResultCollection nugetPackageResultCollection, BuildAndRevision buildAndRevision)
        {
            this.solutionSystemInfo = solutionSystemInfo;

            this.buildAndRevision = buildAndRevision;

            this.nugetPackageResultCollection = nugetPackageResultCollection;
        }

        public WaitStep DumpUpdatedPackages()
        {
            var json = JsonConvert.SerializeObject(nugetPackageResultCollection, Formatting.Indented);

            ConsoleHelper.WriteLine(json);

            var solutionFolderPath = solutionSystemInfo.FolderPath;

            var jsonPath = Path.Combine(solutionFolderPath, "Libraries", $"Hsdk.Packages.Version.{buildAndRevision.Build}.{buildAndRevision.Revision}.json");

            Directory.CreateDirectory(new FileInfo(jsonPath).Directory.FullName);

            var newVersions = nugetPackageResultCollection.NewPackagesVersions;

            var newJsonVersions = JsonConvert.SerializeObject(newVersions, Formatting.Indented);

            File.WriteAllText(jsonPath, newJsonVersions);

            return new WaitStep();
        }
    }
}