namespace SharpTestsEx.ExtensionsImpl
{
	public class ChildConstraint<T, TParentConstraint> : IChildConstraints<T, TParentConstraint>
		where TParentConstraint : IConstraints<T>
	{
		public ChildConstraint(TParentConstraint parent)
		{
			AssertionParent = parent;
		}

		public IAssertionInfo<T> AssertionInfo => AssertionParent.AssertionInfo;

        public TParentConstraint AssertionParent { get; private set; }
	}
}