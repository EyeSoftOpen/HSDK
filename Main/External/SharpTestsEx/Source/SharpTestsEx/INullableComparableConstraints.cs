namespace SharpTestsEx
{
	public interface INullableComparableConstraints<T> : IBeConstraints<IComparableBeConstraints<T>>,
																											 INegableConstraints<INullableComparableConstraints<T>>,
																											 IConstraints<T>, IAllowClone,
																											 IHaveConstraints<IHaveNullableComparableConstraints<T>> { }

	public interface IHaveNullableComparableConstraints<T> : IConstraints<T>
	{
		void Value();
	}
}