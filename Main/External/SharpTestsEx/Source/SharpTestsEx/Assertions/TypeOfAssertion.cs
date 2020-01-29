using System;

namespace SharpTestsEx.Assertions
{
	public class TypeOfAssertion: Assertion<object, Type>
	{
		public TypeOfAssertion(Type expected) : base("Be Type Of", expected, a=> a != null && a.GetType().Equals(expected) )
		{
			if (expected == null)
			{
				throw new ArgumentNullException("expected");
			}
		}
	}
}