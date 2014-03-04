namespace EyeSoft.Transfer.Chunking.Domain.Aggregates
{
	using System;

	using EyeSoft.Domain;

	public class DocumentRequest
		: Aggregate
	{
		public virtual int ChunkSize { get; protected set; }

		public virtual Document Document { get; protected set; }

		public static DocumentRequest Create(Guid id, int chunkSize, Document document)
		{
			return
				new DocumentRequest
					{
						Id = id,
						ChunkSize = chunkSize,
						Document = document
					};
		}
	}
}