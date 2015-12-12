namespace EyeSoft.Nuget.Publisher.Shell.Build
{
	public class SolutionSystemInfo
	{
		public SolutionSystemInfo(string folderPath, string filePath)
		{
			FolderPath = folderPath;
			FilePath = filePath;
		}

		public string FolderPath { get; }

		public string FilePath { get; }
	}
}