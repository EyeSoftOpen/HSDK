namespace EyeSoft.Nuget.Publisher.Core.Nuget
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	using EyeSoft.Nuget.Publisher.Core.Core;

	public static class Packages
	{
		public static Package Parse(FileInfo assemblyInfoFile)
		{
			try
			{
				var lines = Storage.ReadAllLines(assemblyInfoFile.FullName);

				var title = AssemblyInfo.GetData(lines, AssemblyInfoData.AssemblyTitle);

				var projectFile = assemblyInfoFile.Directory.Parent.GetFiles(title + "*.csproj").SingleOrDefault();

				if (projectFile == null)
				{
					return null;
				}

				var targetFramework = ExtractTargetFramework(projectFile);

				var version = AssemblyInfo.GetData(lines, AssemblyInfoData.AssemblyVersion);

				var nuspec = projectFile.Directory.GetFiles("Package.nuspec").SingleOrDefault();

				if (nuspec == null)
				{
					return new Package(projectFile, assemblyInfoFile, lines, title, version, targetFramework);
				}

				return new Package(projectFile, assemblyInfoFile, lines, title, version, targetFramework, nuspec);
			}
			catch (Exception exception)
			{
				throw new IOException($"Cannot parse the file {assemblyInfoFile.FullName}.", exception);
			}
		}

		private static string ExtractTargetFramework(FileInfo projectFile)
		{
			var targetFrameworkElement =
				XElement.Load(projectFile.FullName)
					.Descendants("{http://schemas.microsoft.com/developer/msbuild/2003}TargetFrameworkVersion")
					.SingleOrDefault();

			var targetFramework = "4.0";

			if (targetFrameworkElement != null)
			{
				targetFramework = targetFrameworkElement.Value.Replace("v", null);
			}
			return targetFramework;
		}
	}
}