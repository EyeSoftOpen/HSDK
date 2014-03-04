namespace EyeSoft.Diagnostic
{
	public class BenchmarkReport
	{
		public BenchmarkReport(string operationName, long milliseconds)
		{
			OperationName = operationName;
			Milliseconds = milliseconds;
		}

		public string OperationName { get; private set; }

		public long Milliseconds { get; private set; }

		public double Percentage { get; internal set; }

		public long TimesFaster { get; internal set; }
	}
}