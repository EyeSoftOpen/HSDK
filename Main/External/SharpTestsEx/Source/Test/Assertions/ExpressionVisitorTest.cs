using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class ExpressionVisitorTest
	{
		[Test]
		public void WhenCalledWithAllCostant_IsIvalid()
		{
			var exv = new ExpressionVisitor<int>(2, a => 4 != 5);
			exv.Executing(x => x.Visit()).Throws<InvalidOperationException>().And.ValueOf.Message.Should().Contain("constant");
		}

		[Test]
		public void WhenDoesNotContainActualParameter_IsIvalid()
		{
			int z = 0;
			var exv = new ExpressionVisitor<int>(2, a => z == 5);
			exv.Executing(x => x.Visit()).Throws<InvalidOperationException>().And.ValueOf
				.Message.Should().Not.Contain("constant")
				.And.Contain(".z == 5");
		}

		[Test]
		public void WhenVarAndActual_FindParameter()
		{
			var var2 = 2;
			var exv = new ExpressionVisitor<int>(2, a => var2 == a);
			exv.Executing(x => x.Visit()).NotThrows();
		}

		[Test]
		public void ArrayMemberAndActual_FindParameter()
		{
			var ints = new[] { 1, 2, 3 };
			var exv = new ExpressionVisitor<int>(2, a => ints[1] == a);
			exv.Executing(x => x.Visit()).NotThrows();
		}

		[Test]
		public void WhenValueTypeMethodCall_ManageTheMethodCall()
		{
			var exv = new ExpressionVisitor<string>("1", a => a == 1.ToString());
			exv.Executing(x => x.Visit()).NotThrows();
		}

		[Test]
		public void UsingActualPropertyValue_FindParameter()
		{
			var ints = new[] { 1, 2, 3 };
			var exv = new ExpressionVisitor<int[]>(ints, a => a[0] == 1 && a[1] == 2);
			exv.Executing(x => x.Visit()).NotThrows();
		}

		[Test]
		public void UnaryExpression_FindParameter()
		{
			var exv = new ExpressionVisitor<bool>(false, a => !a);
			exv.Executing(x => x.Visit()).NotThrows();

			exv = new ExpressionVisitor<bool>(true, a => a);
			exv.Executing(x => x.Visit()).NotThrows();

			exv = new ExpressionVisitor<bool>(true, a => !(!a));
			exv.Executing(x => x.Visit()).NotThrows();
		}
	}
}