namespace EyeSoft.IO
{
	using System.IO;
	using System.Linq;

	using EyeSoft.Extensions;

	public static class StorageExtensions
	{
		public static byte[] GetChunk(this IStorage storage, string path, int size, int chunkProgressive)
		{
			Enforce
				.Argument(() => storage)
				.Argument(() => path);

			var fileInfo =
				storage
					.File(path);

			Ensure
				.That((long)size)
				.Is.Not.GreaterThan(fileInfo.Length);

			return
				fileInfo
					.OpenRead()
					.Using(
						stream =>
							{
								stream.Seek(chunkProgressive * size, SeekOrigin.Begin);
								var buffer = new byte[size];
								var readed = stream.Read(buffer, 0, size);
								return buffer.Take(readed).ToArray();
							});
		}
	}
}