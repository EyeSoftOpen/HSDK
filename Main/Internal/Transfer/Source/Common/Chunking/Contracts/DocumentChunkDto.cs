namespace EyeSoft.Transfer.Chunking.Contracts
{
	public class DocumentChunkDto
	{
		public byte[] Data { get; set; }

		public string Sha1 { get; set; }
	}
}