namespace EyeSoft.Transfer.Chunking.Contracts
{
	public class DocumentRequestDto
	{
		public string Path { get; set; }

		public int ChunkSize { get; set; }
	}
}