namespace EyeSoft.Transfer.Service.Chunking
{
	using System;

	using EyeSoft.Extensions;
	using EyeSoft.IO;
	using EyeSoft.Security.Cryptography;
	using EyeSoft.Transfer.Chunking.Contracts;
	using EyeSoft.Transfer.Chunking.Domain.Transactional;

	public class ChunckedDownloadService
	{
		private readonly IStorage storage;
		private readonly ChunkingUnitOfWorkFactory unitOfWorkFactory;

		public ChunckedDownloadService(IStorage storage, ChunkingUnitOfWorkFactory unitOfWorkFactory)
		{
			this.storage = storage;
			this.unitOfWorkFactory = unitOfWorkFactory;
		}

		public DocumentSessionDto RegisterDownload(DocumentRequestDto documentRequestDto)
		{
			using (var unitOfWork = unitOfWorkFactory.Create())
			{
				var document =
					unitOfWork
						.DocumentRepository
						.GetByPath(documentRequestDto.Path);

				if (document.IsNull())
				{
					document = storage.CreateDocument(documentRequestDto.Path);

					unitOfWork
						.DocumentRepository
						.Save(document);
				}

				var documentRequest =
					new DocumentRequestDtoToDocumentRequest(document)
					.Convert(documentRequestDto);

				unitOfWork
					.DocumentRequestRepository
					.Add(documentRequest);

				unitOfWork.Commit();

				var totalChunks =
					Convert
						.ToInt32(Math.Ceiling(document.Size / Convert.ToDouble(documentRequest.ChunkSize)));

				return
					new DocumentSessionDto
						{
							DocumentSessionId = documentRequest.Id,
							DocumentSize = document.Size,
							DocumentSha1 = document.Sha1,
							TotalChunks = totalChunks
						};
			}
		}

		public DocumentChunkDto GetChunck(ChunkRequestDto chunkRequest)
		{
			using (var unitOfWork = unitOfWorkFactory.Create())
			{
				var documentRequest =
					unitOfWork
						.DocumentRequestRepository
						.Load(chunkRequest.DocumentSessionId);

				var data =
					storage
						.GetChunk(
							documentRequest.Document.Path,
							documentRequest.ChunkSize,
							chunkRequest.ChunkProgressive);

				return
					new DocumentChunkDto
						{
							Data = data,
							Sha1 = Hashing.ComputeHex(data)
						};
			}
		}
	}
}