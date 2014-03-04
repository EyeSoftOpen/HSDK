namespace EyeSoft.Transfer.Service.Test.Helpers
{
	using EyeSoft.Transfer.Chunking.Contracts;

	public static class KnownRequestDto
	{
		public static readonly DocumentRequestDto RequestDto =
			new DocumentRequestDto
				{
					Path = KnownFile.Path,
					ChunkSize = 10
				};
	}
}