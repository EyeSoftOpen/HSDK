namespace EyeSoft.Transfer.Chunking.Contracts
{
	using System;

	public class DocumentSessionDto
	{
		public Guid DocumentSessionId { get; set; }

		public long DocumentSize { get; set; }

		public int TotalChunks { get; set; }

		public string DocumentSha1 { get; set; }
	}
}