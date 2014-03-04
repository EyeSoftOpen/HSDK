namespace EyeSoft.Transfer.Streaming
{
	using System.ServiceModel;

	[MessageContract]
	public class StreamUploadResponseDto
	{
		[MessageHeader]
		public int Id { get; set; }
	}
}