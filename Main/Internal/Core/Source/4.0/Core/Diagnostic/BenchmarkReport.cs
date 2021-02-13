namespace EyeSoft.Diagnostic
{
	public class BenchmarkReport
	{
		public BenchmarkReport(string operationName, long milliseconds)
		{
			OperationName = operationName;
			Milliseconds = milliseconds;
		}

		public string OperationName { get; }

		public long Milliseconds { get; }

		public double Percentage { get; internal set; }

		public long TimesFaster { get; internal set; }
	}
}