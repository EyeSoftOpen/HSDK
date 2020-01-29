using System;

namespace SharpTestsEx.Assertions
{
	public interface IAssertionMatcher<TActual>
	{
		Func<TActual, bool> Matcher { get; }
	}
}