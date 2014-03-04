namespace EyeSoft.Transfer.Streaming
{
	using System.ServiceModel;

	[ServiceContract]
	public interface IStreamedService
	{
		[OperationContract]
		StreamDto Download(StreamDownloadRequestDto document);

		[OperationContract]
		StreamUploadResponseDto Upload(StreamDto document);
	}
}