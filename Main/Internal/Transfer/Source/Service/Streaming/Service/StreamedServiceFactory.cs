namespace EyeSoft.Transfer.Service.Streaming.Service
{
	using EyeSoft;
	using EyeSoft.IO;
	using EyeSoft.Transfer.Streaming;

	public class StreamedServiceFactory
		: IStreamedServiceFactory
	{
		public IStreamedService Create(IStorage storage)
		{
			Enforce.Argument(() => storage);

			return
				new StreamedService(storage);
		}
	}
}