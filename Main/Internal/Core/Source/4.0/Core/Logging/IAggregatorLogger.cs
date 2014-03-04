namespace EyeSoft.Logging
{
	public interface IAggregatorLogger : ILogger
	{
		IAggregatorLogger Register(ILogger logger);
	}
}