namespace EyeSoft.Transfer.Service.Chunking
{
	using EyeSoft.IO;
	using EyeSoft.Transfer.Chunking.Domain.Aggregates;

	internal static class StorageExtensions
	{
		public static Document CreateDocument(this IStorage storage, string path)
		{
			var fileInfo = storage.File(path);

			var sha1 = storage.File(path).Hash();

			return Document.Create(path, fileInfo.Length, sha1);
		}
	}
}