namespace EyeSoft.Transfer.Service.Streaming.Service
{
	using EyeSoft.IO;
	using EyeSoft.Transfer.Streaming;

	public partial class StreamedService
		: IStreamedService
	{
		private readonly IStorage storage;

		internal StreamedService(IStorage storage)
		{
			this.storage = storage;
		}
	}
}