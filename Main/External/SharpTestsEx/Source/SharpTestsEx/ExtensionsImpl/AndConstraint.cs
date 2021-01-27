namespace SharpTestsEx.ExtensionsImpl
{
	public class AndConstraint<T> : IAndConstraints<T> where T : class, IAllowClone
	{
		private readonly T rootConstraint;

		public AndConstraint(T rootConstraint)
		{
			this.rootConstraint = rootConstraint;
		}

		public T And => rootConstraint;
    }
}