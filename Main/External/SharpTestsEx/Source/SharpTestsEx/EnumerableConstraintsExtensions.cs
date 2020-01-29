using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpTestsEx.Assertions;
using SharpTestsEx.ExtensionsImpl;

namespace SharpTestsEx
{
	public static class EnumerableConstraintsExtensions
	{
		public static IAndConstraints<IEnumerableConstraints<T>> SameInstanceAs<T>(this IEnumerableBeConstraints<T> constraint, IEnumerable<T> expected)
		{
			constraint.AssertionInfo.AssertUsing(new SameInstanceAssertion<IEnumerable<T>, IEnumerable<T>>(expected));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> Contain<T>(this IEnumerableConstraints<T> constraint,
		                                                                    T expected)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<IEnumerable<T>, T>(Properties.Resources.PredicateContain, expected,
			                                                                      a => (a != null) && a.Contains(expected),
			                                                                      mi =>
			                                                                      string.Format("{0} {1} {2} {3}.{4}",
			                                                                                    Messages.FormatEnumerable(
			                                                                                    	mi.Actual),
			                                                                                    Properties.Resources.AssertionVerb,
			                                                                                    mi.AssertionPredicate,
			                                                                                    Messages.FormatValue(mi.Expected),
			                                                                                    mi.CustomMessage)));
			return ConstraintsHelper.AndChain(constraint);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> SameValuesAs<T>(this IEnumerableHaveConstraints<T> constraint, IEnumerable<T> expected)
		{
			constraint.AssertionInfo.AssertUsing(new SameValuesAsAssertion<T>(expected));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> UniqueValues<T>(this IEnumerableHaveConstraints<T> constraint)
		{
			constraint.AssertionInfo.AssertUsing(
				new Assertion<IEnumerable<T>, IEnumerable<T>>(Properties.Resources.PredicateHaveUniqueValues, null, 
					a =>
					{
						if (a == null)
						{
							return false;
						}
						var unique = new HashSet<T>(a);
						return unique.Count == a.Count();
					},
				 new UnaryEnumerableMessageBuilder<T>(a =>
                                            	from gElem in (from elem in a group elem by elem)
                                            	where gElem.Count() > 1
                                            	select gElem.Key)));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> SubsetOf<T>(this IEnumerableBeConstraints<T> constraint, IEnumerable<T> expected)
		{
			constraint.AssertionInfo.AssertUsing(new SubsetOfAssertion<T>(expected));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> Ordered<T>(this IEnumerableBeConstraints<T> constraint)
		{
			constraint.AssertionInfo.AssertUsing(
				new Assertion<IEnumerable<T>, IEnumerable<T>>(Properties.Resources.PredicateBeOrdered, null,
				                                              a =>
				                                              a != null
				                                              &&
				                                              (a.SequenceEqual(from elem in a orderby elem select elem)
				                                               ||
				                                               a.SequenceEqual(from elem in a orderby elem descending select elem)),
				                                              new UnaryEnumerableMessageBuilder<T>()));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> SameSequenceAs<T>(this IEnumerableHaveConstraints<T> constraint, params T[] expected)
		{
			constraint.AssertionInfo.AssertUsing(new SameSequenceAsAssertion<T>(expected));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> SubsetOf<T>(this IEnumerableBeConstraints<T> constraint, params T[] expected)
		{
			constraint.AssertionInfo.AssertUsing(new SubsetOfAssertion<T>(expected));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> SameValuesAs<T>(this IEnumerableHaveConstraints<T> constraint, params T[] expected)
		{
			constraint.AssertionInfo.AssertUsing(new SameValuesAsAssertion<T>(expected));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> OrderedAscending<T>(this IEnumerableBeConstraints<T> constraint)
		{
			constraint.AssertionInfo.AssertUsing(new OrderedAscendingAssertion<T>());
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> OrderedDescending<T>(this IEnumerableBeConstraints<T> constraint)
		{
			constraint.AssertionInfo.AssertUsing(new OrderedDescendingAssertion<T>());
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IEnumerableConstraints<T>> OrderedBy<T,TKey>(this IEnumerableBeConstraints<T> constraint, Expression<Func<T,TKey>> keySelector)
		{
			var compiledFunc = keySelector.Compile();
			constraint.AssertionInfo.AssertUsing(
				new Assertion<IEnumerable<T>, IEnumerable<T>>(string.Format(Properties.Resources.PredicateBeOrderedBy, (new ExpressionStringBuilder(keySelector))), constraint.AssertionInfo.Actual.OrderBy(compiledFunc),
																											a =>
																											a != null
																											&&
																											a.SequenceEqual(a.OrderBy(compiledFunc)),
																											new OrderedMessageBuilder<T>()));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}
	}
}