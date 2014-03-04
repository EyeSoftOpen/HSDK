namespace EyeSoft.Transfer.Service.Streaming.Service
{
	using EyeSoft.IO;
	using EyeSoft.Security.Cryptography;
	using EyeSoft.Transfer.Streaming;

	public partial class StreamedService
	{
		public StreamDto Download(StreamDownloadRequestDto document)
		{
			var fileInfo =
				storage
					.File(document.Key);

			var stream = fileInfo.OpenRead();

			return
				new StreamDto
					{
						DocumentName = document.Key,
						Sha1 = Hashing.ComputeHex(stream),
						Length = fileInfo.Length,
						Stream = stream
					};
		}
	}
}