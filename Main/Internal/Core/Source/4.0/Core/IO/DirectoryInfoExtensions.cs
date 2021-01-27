namespace EyeSoft.Core.IO
{
    using System.IO;
    using System.Linq;

    public static class DirectoryInfoExtensions
	{
		public static bool IsEmpty(this IDirectoryInfo directoryInfo)
		{
			const SearchOption AllDirectories = SearchOption.AllDirectories;

			var anyFile = directoryInfo.GetFiles(Storage.AllPattern, AllDirectories).Any();
			var anyDirectory = directoryInfo.GetFiles(Storage.AllPattern, AllDirectories).Any();

			return !anyDirectory && !anyFile;
		}

		public static void DeleteIfEmpty(this IDirectoryInfo directoryInfo)
		{
			if (!directoryInfo.Exists)
			{
				return;
			}

			if (!directoryInfo.IsEmpty())
			{
				return;
			}

			directoryInfo.Delete(true);
		}
	}
}