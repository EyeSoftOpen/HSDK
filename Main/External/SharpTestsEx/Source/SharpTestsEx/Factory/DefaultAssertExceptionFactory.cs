using System;

namespace SharpTestsEx.Factory
{
	public static class AssertExceptionFactory
	{
		public static Exception CreateException(string message)
		{
			return new AssertException(message);
		}
	}
}
