namespace EyeSoft.Core.Diagnostic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using Collections.Generic;

    public class BenchmarkActionScheduler
        : IBenchmarkActionScheduler
    {
        private readonly int count;

        private readonly IDictionary<string, Action> actionDictionary =
            new Dictionary<string, Action>();

        private int actionIndex;

        internal BenchmarkActionScheduler(int count)
        {
            this.count = count;
        }

        public IBenchmarkActionScheduler Execute(Action action)
        {
            return Execute((actionIndex++).ToString(CultureInfo.InvariantCulture), action);
        }

        public IBenchmarkActionScheduler Execute(string name, Action action)
        {
            actionDictionary.Add(name, action);
            return this;
        }

        IEnumerable<BenchmarkReport> IBenchmarkActionScheduler.Report(int? digits)
        {
            var reportList = new List<BenchmarkReport>();

            foreach (var action in actionDictionary)
            {
                var stopwatch = Stopwatch.StartNew();

                var localAction = action;

                Enumerable
                    .Range(0, count)
                    .Iterate(() => localAction.Value());

                reportList
                    .Add(new BenchmarkReport(action.Key, stopwatch.ElapsedMilliseconds));
            }

            var maxTime = reportList.Max(x => x.Milliseconds);

            foreach (var report in reportList)
            {
                var percentage = (report.Milliseconds * 100) / (double)maxTime;

                if (digits.HasValue)
                {
                    percentage = Math.Round(percentage, digits.Value);
                }

                report.Percentage = percentage;
                report.TimesFaster = (int)Math.Round(maxTime / (double)report.Milliseconds);
            }

            return reportList;
        }
    }
}