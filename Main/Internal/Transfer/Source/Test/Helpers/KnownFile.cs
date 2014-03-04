namespace EyeSoft.Transfer.Service.Helpers
{
	using System.IO;

	using EyeSoft.Testing.IO;

	internal static class KnownFile
	{
		public const string Sha1 = "5f1de5c39655f34c149bdbbba70d0e1ec5a86eb5";

		public const string Path = "test/document1.txt";

		public const int TotalChunks = 4;

		public static readonly int DocumentSize = Content.Length;

		private const string Content = "Test text content fot the file.";

		public static Stream Stream
		{
			get
			{
				return
					new RandomStream(DocumentSize, false, Path.GetHashCode());
			}
		}
	}
}