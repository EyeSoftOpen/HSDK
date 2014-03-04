namespace EyeSoft.Transfer.Streaming
{
	using System.IO;
	using System.ServiceModel;

	[MessageContract]
	public class StreamDto
	{
		[MessageHeader]
		public virtual long Length { get; set; }

		[MessageHeader]
		public virtual string Email { get; set; }

		[MessageHeader]
		public virtual string DocumentName { get; set; }

		[MessageHeader]
		public virtual string Sha1 { get; set; }

		[MessageBodyMember]
		public virtual Stream Stream { get; set; }
	}
}