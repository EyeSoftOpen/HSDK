namespace EyeSoft.Core.Diagnostic
{
	public interface IDebugger
	{
		bool IsAttached { get; }

		bool IsLogging();
	}
}