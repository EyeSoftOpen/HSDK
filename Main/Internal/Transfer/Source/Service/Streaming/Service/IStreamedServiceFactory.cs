namespace EyeSoft.Transfer.Service.Streaming.Service
{
	using EyeSoft.IO;
	using EyeSoft.Transfer.Streaming;

	public interface IStreamedServiceFactory
	{
		IStreamedService Create(IStorage storage);
	}
}