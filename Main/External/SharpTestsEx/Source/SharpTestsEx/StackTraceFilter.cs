using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpTestsEx
{
    public static class StackTraceFilter
    {
        private static readonly Regex stackTraceFilter = new Regex(@"SharpTestsEx(?!\.Tests)\.");

        public static string FilterStackTrace(string stackTrace)
        {
            var sb = new StringBuilder();
            var sr = new StringReader(stackTrace);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (!stackTraceFilter.IsMatch(line))
                {
                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }
    }
}
