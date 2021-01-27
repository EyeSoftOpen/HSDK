namespace EyeSoft.Core.IO
{
    using System;
    using System.IO;

    public static class PathExtensions
	{
		private static readonly char directorySeparatorChar = Path.DirectorySeparatorChar;

		public static bool Equals(string sourcePath, string comparePath)
		{
			var sourceTrimmed = sourcePath.TrimPath();
			var compareTrimmed = comparePath.TrimPath();

			return sourceTrimmed.Equals(compareTrimmed, StringComparison.InvariantCultureIgnoreCase);
		}

		public static string TrimPath(this string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}

			return path.TrimEnd(directorySeparatorChar);
		}
	}
}