using System;
using System.Linq;
using SharpTestsEx.Assertions;
using System.Reflection;

namespace SharpTestsEx.ExtensionsImpl
{
	public class TypeConstraints : NegableConstraints<Type, ITypeConstraints>, ITypeConstraints
	{
		public TypeConstraints(Type actual) : base(actual) { }
		public TypeConstraints(Type actual, Func<string> messageBuilder) : base(actual, messageBuilder) { }

		#region Implementation of IBeConstraints<ITypeBeConstraints>

		public ITypeBeConstraints Be
		{
			get { return new TypeBeConstraints(this); }
		}

		#endregion

		#region Implementation of IHaveConstraints<ITypeHaveConstraints>

		public ITypeHaveConstraints Have
		{
			get { return new TypeHaveConstraints(this); }
		}

		#endregion

		#region Implementation of IAllowClone

		public object Clone()
		{
			return new TypeConstraints(AssertionInfo.Actual, () => AssertionInfo.FailureMessage);
		}

		#endregion
	}

	public class TypeBeConstraints : ChildAndChainableConstraints<Type, ITypeConstraints>, ITypeBeConstraints
	{
		public TypeBeConstraints(ITypeConstraints parent) : base(parent) { }

		#region Implementation of ITypeBeConstraints

		/// <summary>
		/// Verifies that the <see cref="Type"/> instance is null.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		public IAndConstraints<ITypeConstraints> Null()
		{
			AssertionInfo.AssertUsing(new NullAssertion<Type>());
			return AndChain;
		}

		public IAndConstraints<ITypeConstraints> EqualTo<T>()
		{
			return EqualTo(typeof(T));
		}

		public IAndConstraints<ITypeConstraints> EqualTo(Type expected)
		{
			AssertionInfo.AssertUsing(new ObjectEqualsAssertion<Type, Type>(expected));
			return AndChain;
		}

		public IAndConstraints<ITypeConstraints> AssignableFrom(Type expected)
		{
			AssertionInfo.AssertUsing(new Assertion<Type, Type>(Properties.Resources.PredicateBeAssignableFrom,expected, t => t.GetTypeInfo().IsAssignableFrom(expected)));
			return AndChain;
		}

		public IAndConstraints<ITypeConstraints> AssignableFrom<T>()
		{
			return AssignableFrom(typeof(T));
		}

		public IAndConstraints<ITypeConstraints> SubClassOf(Type expected)
		{
			AssertionInfo.AssertUsing(new Assertion<Type, Type>(Properties.Resources.PredicateBeSubClassOf,expected, t => t.GetTypeInfo().IsSubclassOf(expected)));
			return AndChain;
		}

		public IAndConstraints<ITypeConstraints> SubClassOf<T>()
		{
			return SubClassOf(typeof(T));
		}

		#endregion
	}

	public class TypeHaveConstraints : ChildAndChainableConstraints<Type, ITypeConstraints>, ITypeHaveConstraints
	{
		public TypeHaveConstraints(ITypeConstraints parent) : base(parent) { }

		#region Implementation of ITypeHaveConstraints

		public IAndConstraints<ITypeConstraints> Attribute<T>()
		{
			AssertionInfo.AssertUsing(new Assertion<Type, Type>(Properties.Resources.PredicateHaveAttribute,typeof(T),
																															t => (t != null) && t.GetTypeInfo().GetCustomAttributes(true).Select(a => a.GetType()).Contains(typeof(T))));
			return AndChain;
		}

		#endregion
	}
}