namespace EyeSoft.Diagnostic
{
    using System;
    using System.Collections.Generic;

    public interface IBenchmarkActionScheduler
	{
		IEnumerable<BenchmarkReport> Report(int? digits = null);

		IBenchmarkActionScheduler Execute(Action action);

		IBenchmarkActionScheduler Execute(string name, Action action);
	}
}