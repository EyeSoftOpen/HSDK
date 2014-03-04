namespace EyeSoft.Transfer.Chunking.Contracts
{
	using System;

	using EyeSoft;
	using EyeSoft.Transfer.Chunking.Domain.Aggregates;

	public class DocumentRequestDtoToDocumentRequest
		: IConverter<DocumentRequestDto, DocumentRequest>
	{
		private readonly Document document;

		public DocumentRequestDtoToDocumentRequest(Document document)
		{
			this.document = document;
		}

		public DocumentRequest Convert(DocumentRequestDto source)
		{
			return
				DocumentRequest
					.Create(
						Guid.NewGuid(),
						source.ChunkSize,
						document);
		}
	}
}