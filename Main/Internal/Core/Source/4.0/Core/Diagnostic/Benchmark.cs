namespace EyeSoft.Diagnostic
{
    using System;
    using System.Diagnostics;

    public static class Benchmark
    {
        public static TimeSpan For(int count, Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < count; i++)
            {
                action();
            }

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public static BenchmarkActionScheduler For(int count)
        {
            return new BenchmarkActionScheduler(count);
        }
    }
}