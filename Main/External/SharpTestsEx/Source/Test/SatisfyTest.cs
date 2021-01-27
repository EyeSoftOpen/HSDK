namespace SharpTestsEx.Test
{
    using System.Linq;
    using NUnit.Framework;

    public class SatisfyTest
	{
		public enum MyEnum
		{
			One,
			Two
		}
		public class WithMethod
		{
			public bool MethodCall(bool returnValue)
			{
				return returnValue;
			}

			public bool IsOpen { get; set; }
			public MyEnum EnumValue { get; set; }
		}

		[Test]
		public void EqualTo()
		{
			1.Satisfies(a => a == 1);
			Executing.This(() => 2.Satisfies(a => a == 1)).Should().Throw<AssertException>();
		}

		[Test]
		public void EqualToInvertingPosition()
		{
			1.Satisfies(a => 1 == a);
			Executing.This(() => 2.Satisfies(a => 1 == a)).Should().Throw<AssertException>();
		}

		[Test]
		public void SillyExpressionUsingActualInBothPositions()
		{
#pragma warning disable 1718
			1.Satisfies(a => a == a);
			Executing.This(() => 2.Satisfies(a => a != a)).Should().Throw<AssertException>();
#pragma warning restore 1718
		}

		[Test]
		public void EqualToWithAnd()
		{
			1.Satisfies(a => a == 1 && a != 0);
			Executing.This(() => 2.Satisfies(a => a != 1 && a != 2)).Should().Throw<AssertException>();
		}

		[Test]
		public void EqualToWithOr()
		{
			1.Satisfies(a => a == 1 || a != 0);
			Executing.This(() => 2.Satisfies(a => a == 1 || a == 3)).Should().Throw<AssertException>();
		}

		[Test]
		public void NotEqual()
		{
			1.Satisfies(a => a != 0);
			Executing.This(() => 1.Satisfies(a => a != 1)).Should().Throw<AssertException>();
		}

		[Test]
		public void GreaterThan()
		{
			1.Satisfies(a => a > 0);
			Executing.This(() => 1.Satisfies(a => a > 2)).Should().Throw<AssertException>();
		}

		[Test]
		public void GreaterThanOrEqualTo()
		{
			1.Satisfies(a => a >= 0);
			Executing.This(() => 1.Satisfies(a => a >= 2)).Should().Throw<AssertException>();
		}

		[Test]
		public void LessThan()
		{
			1.Satisfies(a => a < 2);
			Executing.This(() => 1.Satisfies(a => a < 0)).Should().Throw<AssertException>();
		}

		[Test]
		public void LessThanOrEqualTo()
		{
			1.Satisfies(a => a <= 2);
			Executing.This(() => 1.Satisfies(a => a <= 0)).Should().Throw<AssertException>();
		}

		[Test]
		public void EvaluateMathOperations()
		{
			var var2 = 2;
			var var4 = 4;
			var zero = 0;

			4.Satisfies(a => var2 * var2 == a);
			2.Satisfies(a => a == var4 / var2);
			4.Satisfies(a => a == var2 + var2);
			2.Satisfies(a => var4 - var2 == a);

			4.Satisfies(a => a - zero == a);
		}

		[Test]
		public void UnaryExpression()
		{
			5.Satisfies(a => !(a > 6));
			Executing.This(() => 5.Satisfies(a => !(a > 4))).Should().Throw<AssertException>();
		}

		[Test]
		public void MethodCall()
		{
			(new[] {1, 2}).Satisfies(a => a.Contains(2));
			(new[] {1, 2}).Satisfies(a => a.Where(x => x > 0).Contains(2));
			(new[] { 2, 3, 3, 4 }).Satisfies(a => a.All(x => x > 1));
			(new[] { 0, 0, 1 }).Satisfies(a => a.Any(x => x > 0));

			Executing.This(() => (new[] { 1 }).Satisfies(a => a.Contains(2))).Should().Throw<AssertException>().And.ValueOf
			.Message.Should().Contain("Satisfy").And.Contain("a.Contains(2)");
		}

		[Test]
		public void InstanceMethodCall()
		{
			"something".Satisfies(a => a.Contains("th"));
			"something".Satisfies(a => a.ToUpperInvariant().Contains("TH"));

			(new WithMethod()).Satisfies(a => a.MethodCall(true));
			Executing.This(() => (new WithMethod()).Satisfies(a => a.MethodCall(false))).Should().Throw<AssertException>().And.ValueOf
			.Message.ToLowerInvariant().Should().Contain("satisfy").And.Contain("a.methodcall(false)");
		}

		[Test]
		public void InstancePropertyCall()
		{
			var actual = (new WithMethod());
			actual.IsOpen = true;
			actual.Satisfies(a => a.IsOpen);
			actual.IsOpen = false;
			Executing.This(() => actual.Satisfies(a => a.IsOpen)).Should().Throw<AssertException>().And.ValueOf
			.Message.ToLowerInvariant().Should().Contain("satisfy").And.Contain("a.isopen");
		}

		[Test]
		public void WintLinq()
		{
			var ints = new[] {1, 2, 3};
			ints.All(x => x.Satisfies(a => a > 0));
			ints.Satisfies(a => a.All(x => x > 0));

			Executing.This(() => ints.All(x => x.Satisfies(a => a < 3))).Should().Throw<AssertException>();
			Executing.This(() => ints.Satisfies(a => a.All(x => x < 3))).Should().Throw<AssertException>();

			ints.Any(x => x.Satisfies(a => a <= 2));
			Executing.This(() => ints.Any(x => x.Satisfies(a => a > 5))).Should().Throw<AssertException>();
		}

		[Test]
		public void WhenActualIsPartOfMath_FindTheParameter()
		{
			2.Executing(x => x.Satisfies(a => 0 == a % 2)).NotThrows();
		}

		//[Test]
		//public void WhenPredicateDoesNotFail_ShouldNotCallVisitor()
		//{
		// This feature is the cause of Issue5225
		//  // if the ExpressionVisitor is called the satisfy fail because invalid predicate
		//  ActionAssert.NotThrow(() => 5.Satisfy(a => true));
		//}

		[Test, Description("http://sharptestex.codeplex.com/WorkItem/View.aspx?WorkItemId=5225")]
		public void MethodWithDifferentResultAtEachExcution()
		{
			Executing.This(() => (new Issue5225(false)).Satisfies(i => i.Match())).Should().Throw<AssertException>();
			Executing.This(() => (new Issue5225(true)).Satisfies(i => i.Match())).Should().NotThrow();
		}
		private class Issue5225
		{
			private bool initialState;

			public Issue5225(bool initialState)
			{
				this.initialState = initialState;
			}

			public bool Match()
			{
				var result = initialState;
				if(!initialState)
				{
					initialState = !initialState;
				}
				return result;
			}
		}

		[Test]
		public void WhenCalledWithTitleThenTheMessageShouldContainCustomTitle()
		{
			Executing.This(() => 2.Satisfies("My custom message", a => a == 1)).Should().Throw<AssertException>().And.ValueOf.
				Message.Should().Contain("My custom message");
		}

		[Test]
		public void InstanceEnumPropertyCall()
		{
			var actual = (new WithMethod());
			actual.EnumValue = MyEnum.One;
			actual.Satisfies(a => a.EnumValue == MyEnum.One);
			actual.EnumValue = MyEnum.Two;
			Executing.This(() => actual.Satisfies(a => a.EnumValue == MyEnum.One)).Should().Throw<AssertException>().And.ValueOf
			.Message.ToLowerInvariant().Should().Contain("satisfy").And.Contain("a.enumvalue");
		}

		[Test]
		public void UsingConstantShouldMagnifyFailure()
		{
			var quantity = 2;
			Executing.This(() => 22.Satisfies(x => x == quantity + 1)).Should().Throw<AssertException>().And.ValueOf
				.Message.ToLowerInvariant().Should().Contain("satisfy").And.Contain("quantity").And.Contain("22").And.Contain("3");
		}

		private interface IInterface{}
		private class MyImpl : IInterface { }

		[Test]
		public void WhenObjectImplementInterfaceThenIsOperatorNotFail()
		{
			object myImpl = new MyImpl();
			myImpl.Satisfies(a => a is IInterface);
		}

		[Test]
		public void WhenObjectNotImplementsInterfaceThenIsOperatorFail()
		{
			var o = new object();
			Executing.This(() => o.Satisfies(a => a is IInterface)).Should().Throw<AssertException>().And.ValueOf
				.Message.Should().Contain("is IInterface");
		}

		[Test]
		public void WhenObjectImplementInterfaceThenAsOperatorNotFail()
		{
			object myImpl = new MyImpl();
			myImpl.Satisfies(a => (a as IInterface) != null);
		}

		[Test]
		public void WhenObjectNotImplementsInterfaceThenAsOperatorFail()
		{
			var o = new object();
			Executing.This(() => o.Satisfies(a => (a as IInterface) != null)).Should().Throw<AssertException>().And.ValueOf
				.Message.Should().Contain("as IInterface");
		}
	}
}