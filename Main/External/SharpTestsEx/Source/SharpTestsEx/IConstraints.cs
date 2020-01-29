using System;

namespace SharpTestsEx
{
	/// <summary>
	/// Basic contract for a generic constraint.
	/// </summary>
	/// <typeparam name="T">The type of the 'actual' value subject of the test.</typeparam>
	public interface IConstraints<T>
	{
		IAssertionInfo<T> AssertionInfo { get; }
	}

	public interface INegableConstraints<TConstraint> where TConstraint : class
	{
		TConstraint Not { get; }
	}

	public interface IBeConstraints<TConstraint> where TConstraint : class
	{
		TConstraint Be { get; }
	}

	public interface IHaveConstraints<TConstraint> where TConstraint : class
	{
		TConstraint Have { get; }
	}

	public interface IAndConstraints<TConstraints> where TConstraints : class, IAllowClone
	{
		TConstraints And { get; }
	}

	public interface IChildConstraints<T, TParentConstraint> : IConstraints<T> where TParentConstraint : IConstraints<T>
	{
		TParentConstraint AssertionParent { get; }
	}

	public interface IChildAndChainableConstraints<T, TParentConstraint> : IChildConstraints<T, TParentConstraint>
		where TParentConstraint : class, IConstraints<T>, IAllowClone { }
}