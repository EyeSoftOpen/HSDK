namespace EyeSoft.Transfer.Streaming
{
	using System.ServiceModel;

	[MessageContract]
	public class StreamDownloadRequestDto
	{
		[MessageHeader]
		public string Key { get; set; }
	}
}