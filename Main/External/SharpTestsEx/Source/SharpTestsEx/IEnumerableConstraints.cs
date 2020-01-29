using System.Collections.Generic;

namespace SharpTestsEx
{
	public interface IEnumerableConstraints<T> : IBeConstraints<IEnumerableBeConstraints<T>>,
	                                             IHaveConstraints<IEnumerableHaveConstraints<T>>,
	                                             INegableConstraints<IEnumerableConstraints<T>>,
																							 IConstraints<IEnumerable<T>>, IAllowClone { }

	public interface IEnumerableBeConstraints<T> : IChildAndChainableConstraints<IEnumerable<T>, IEnumerableConstraints<T>>
	{
		IAndConstraints<IEnumerableConstraints<T>> Null();
		IAndConstraints<IEnumerableConstraints<T>> Empty();
	}

	public interface IEnumerableHaveConstraints<T> :
		IChildAndChainableConstraints<IEnumerable<T>, IEnumerableConstraints<T>>
	{
		IAndConstraints<IEnumerableConstraints<T>> SameSequenceAs(IEnumerable<T> expected);
		IComparableBeConstraints<int> Count { get; }
	}
}