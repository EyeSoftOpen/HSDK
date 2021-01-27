namespace EyeSoft.Core.Logging
{
	public interface IAggregatorLogger : ILogger
	{
		IAggregatorLogger Register(ILogger logger);
	}
}