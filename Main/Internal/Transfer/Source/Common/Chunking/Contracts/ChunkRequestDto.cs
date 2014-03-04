namespace EyeSoft.Transfer.Chunking.Contracts
{
	using System;

	public class ChunkRequestDto
	{
		public Guid DocumentSessionId { get; set; }

		public int ChunkProgressive { get; set; }
	}
}