namespace EyeSoft.Diagnostic
{
	public interface IDebugger
	{
		bool IsAttached { get; }

		bool IsLogging();
	}
}