using System.Text.RegularExpressions;
using SharpTestsEx.Assertions;
using SharpTestsEx.ExtensionsImpl;

namespace SharpTestsEx
{
	public static class StringConstraintsExtensions
	{
		public static IAndConstraints<IStringConstraints> Contain(this IStringConstraints constraint, string expected)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<string, string>(Properties.Resources.PredicateContain, expected,
			                                                                   a => a != null && a.Contains(expected)));
			return ConstraintsHelper.AndChain(constraint);
		}

		public static IAndConstraints<IStringConstraints> StartWith(this IStringConstraints constraint, string expected)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<string, string>("Start with", expected,
																																					a => a != null && a.StartsWith(expected)));
			return ConstraintsHelper.AndChain(constraint);
		}

		public static IAndConstraints<IStringConstraints> EndWith(this IStringConstraints constraint, string expected)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<string, string>("End with", expected, a => a != null && a.EndsWith(expected)));
			return ConstraintsHelper.AndChain(constraint);
		}

		public static IAndConstraints<IStringConstraints> Match(this IStringConstraints constraint, string pattern)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<string, string>("Match", pattern, a => a != null && Regex.IsMatch(a, pattern)));
			return ConstraintsHelper.AndChain(constraint);
		}

		public static IAndConstraints<IStringConstraints> Match(this IStringConstraints constraint, Regex regex)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<string, Regex>("Match RegEx", regex, a => a != null && regex.IsMatch(a),
			                                                                   mi =>
			                                                                   string.Format(
			                                                                   	"{0} {1} {2} (Pattern'{3}' - {4}).{5}",
																																					Messages.FormatValue(mi.Actual),
			                                                                   	Properties.Resources.AssertionVerb,
			                                                                   	mi.AssertionPredicate,
			                                                                   	regex.FieldValue<string>("pattern"),
			                                                                   	regex.Options.ToString(), mi.CustomMessage)));
			return ConstraintsHelper.AndChain(constraint);
		}

		public static IAndConstraints<IStringConstraints> NullOrEmpty(this IStringBeConstraints constraint)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<string, string>("Be Null Or Empty", string.Empty, a => string.IsNullOrEmpty(a)));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IStringConstraints> Be(this IStringConstraints constraint, string expected)
		{
			return constraint.Be.EqualTo(expected);
		}
	}
}