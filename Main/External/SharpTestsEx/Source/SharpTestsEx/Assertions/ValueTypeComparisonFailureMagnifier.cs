using System;
using System.Linq.Expressions;

namespace SharpTestsEx.Assertions
{
	public class ValueTypeComparisonFailureMagnifier : IFailureMagnifier
	{
		private readonly Func<object> leftGetter;
		private readonly ExpressionType nodeType;
		private readonly Func<object> rigthGetter;

		public ValueTypeComparisonFailureMagnifier(ExpressionType nodeType, Func<object> leftGetter, Func<object> rigthGetter)
		{
			this.nodeType = nodeType;
			this.leftGetter = leftGetter;
			this.rigthGetter = rigthGetter;
		}

		#region IFailureMagnifier Members

		public string Message()
		{
			return string.Format("Compared values was: {0} {1} {2}", leftGetter(), ExpressionStringBuilder.ToStringOperator(nodeType), rigthGetter());
		}

		#endregion
	}
}