using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpTestsEx
{
	public class AssertException : Exception
	{
		private readonly Regex stackTraceFilter = new Regex(@"SharpTestsEx(?!\.Tests)(?!\.NUnitTests)\.");

		public AssertException(string message)
			: base(message) {}

		public override string StackTrace
		{
			get
			{
				var sb = new StringBuilder();
				var sr = new StringReader(base.StackTrace);

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
}