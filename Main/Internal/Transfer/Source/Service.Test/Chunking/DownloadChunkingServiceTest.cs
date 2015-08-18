namespace EyeSoft.Transfer.Service.Test.Chunking
{
	using EyeSoft.Security.Cryptography;
	using EyeSoft.Transfer.Chunking.Contracts;
	using EyeSoft.Transfer.Chunking.Domain.Transactional;
	using EyeSoft.Transfer.Service.Chunking;
	using EyeSoft.Transfer.Service.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class DownloadChunkingServiceTest
	{
		[TestMethod]
		public void CheckDownloadDocumentMetadata()
		{
			var unitOfWorkFactory =
				new ChunkingUnitOfWorkFactory(new MemoryTransactionalCollectionFactory());

			var documentMetadata = RegisterDocumentRequest(unitOfWorkFactory);

			CheckDocumentRequestIsSaved(unitOfWorkFactory, documentMetadata);
		}

		[TestMethod]
		public void CheckDownloadedDocument()
		{
			var unitOfWorkFactory =
				new ChunkingUnitOfWorkFactory(new MemoryTransactionalCollectionFactory());

			var documentMetadata = RegisterDocumentRequest(unitOfWorkFactory);

			using (var testStream = KnownFile.Stream)
			{
				for (var chunkProgressive = 0; chunkProgressive < documentMetadata.TotalChunks; chunkProgressive++)
				{
					var chunk =
						CheckChunk(chunkProgressive, unitOfWorkFactory, documentMetadata);

					var buffer = new byte[chunk.Data.Length];
					testStream.Read(buffer, 0, chunk.Data.Length);

					buffer
						.Should().Have.SameSequenceAs(chunk.Data);
				}

				testStream.Position = 0;

				Hashing.ComputeHex(testStream)
					.Should().Be.EqualTo(KnownFile.Sha1);
			}
		}

		private DocumentChunkDto CheckChunk(
			int chunkProgressive,
			ChunkingUnitOfWorkFactory unitOfWorkFactory,
			DocumentSessionDto documentMetadata)
		{
			var chunkRequestDto = new ChunkRequestDto
				{
					DocumentSessionId = documentMetadata.DocumentSessionId,
					ChunkProgressive = chunkProgressive
				};

			var service =
				new ChunckedDownloadService(KnownStorage.Mock, unitOfWorkFactory);

			var chunk =
				service.GetChunck(chunkRequestDto);

			return chunk;
		}

		private DocumentSessionDto RegisterDocumentRequest(ChunkingUnitOfWorkFactory unitOfWorkFactory)
		{
			var downloadService = new ChunckedDownloadService(KnownStorage.Mock, unitOfWorkFactory);

			var documentMetadata =
				downloadService
					.RegisterDownload(KnownRequestDto.RequestDto);

			documentMetadata
				.DocumentSize
				.Should().Be.EqualTo(KnownFile.DocumentSize);

			documentMetadata
				.DocumentSha1
				.Should().Be.EqualTo(KnownFile.Sha1);

			documentMetadata
				.TotalChunks
				.Should().Be.EqualTo(KnownFile.TotalChunks);

			return documentMetadata;
		}

		private void CheckDocumentRequestIsSaved(
			ChunkingUnitOfWorkFactory unitOfWorkFactory,
			DocumentSessionDto documentMetadata)
		{
			using (var unitOfWork = unitOfWorkFactory.Create())
			{
				unitOfWork
					.DocumentRequestRepository
					.Load(documentMetadata.DocumentSessionId)
					.Should().Not.Be.Null();
			}
		}
	}
}