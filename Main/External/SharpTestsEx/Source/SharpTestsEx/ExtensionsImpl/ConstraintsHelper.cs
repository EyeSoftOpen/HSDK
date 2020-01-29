using System;

namespace SharpTestsEx.ExtensionsImpl
{
	public class ConstraintsHelper
	{
		private ConstraintsHelper() { }

		public static IAndConstraints<TParentConstraint> AndChain<TParentConstraint>(TParentConstraint parent)
			where TParentConstraint : class, IAllowClone
		{
			return new AndConstraint<TParentConstraint>((TParentConstraint)parent.Clone());
		}
	}
}