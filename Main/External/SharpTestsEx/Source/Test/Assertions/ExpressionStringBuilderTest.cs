using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class ExpressionStringBuilderTest
	{
		[Test]
		public void NullExpress()
		{
			var eb = new ExpressionStringBuilder(null);
			eb.ToString().Should().Be.EqualTo("(null)");
		}

		[Test]
		public void LocalConstant()
		{
			var expected = 5;
			Expression<Func<int>> expr = ()=>expected;

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => .expected");
		}

		[Test]
		public void ExpressionEqual()
		{
			var expected = 5;
			Expression<Func<int, bool>> expr = a => expected == 5;

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => .expected == 5");
		}

		[Test]
		public void EqualString()
		{
			var expected = "something";
			Expression<Func<string, bool>> expr = a => expected == "algo";

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => .expected == \"algo\"");
		}

		[Test]
		public void EqualNull()
		{
			var expected = "something";
			Expression<Func<string, bool>> expr = a => expected == null;

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => .expected == (null)");			
		}

		[Test]
		public void ExpressionEqualAnd()
		{
			var expected = 5;
			Expression<Func<int, bool>> expr = a => expected == 5 && expected >= 4;

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => .expected == 5 && .expected >= 4");
		}

		[Test]
		public void ExpressionEqualAndOr()
		{
			var expected = 5;
			Expression<Func<int, bool>> expr = a => (expected == 5 && expected >= 4) || expected > 0;

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => (.expected == 5 && .expected >= 4) || .expected > 0");

			expr = a => expected > 0 ||(expected == 5 && expected >= 4);

			eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => .expected > 0 || (.expected == 5 && .expected >= 4)");
		}

		[Test]
		public void Array()
		{
			var ints = new[] { 1, 2, 3 };
			Expression<Func<int, bool>> expr = a => ints[1] == a;

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => .ints[1] == a");
		}

		[Test]
		public void MethodsCalls()
		{
			Expression<Func<string, bool>> expr = a => a.ToUpperInvariant().Contains("TH");
			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => a.ToUpperInvariant().Contains(\"TH\")");
		}

		[Test]
		public void MethodsCallsWithEnumerable()
		{
			Expression<Func<string, bool>> expr = a => a.ToUpperInvariant().StartsWith("algo", StringComparison.CurrentCulture);
			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => a.ToUpperInvariant().StartsWith(\"algo\", StringComparison.CurrentCulture)");
		}

		[Test]
		public void Unary()
		{
			Expression<Func<int, bool>> expr = a => a > -5;
			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => a > -5");

			var myvar = 5;
			expr = a => a > -myvar;
			eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => a > -.myvar");

			expr = a => (a as object) != null;
			eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => (a as Object) != (null)");


			expr = a => !(a > -5);
			eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => !(a > -5)");
		}

		[Test]
		public void ActionCall()
		{
			Expression<Action> expr = () => AMethod();
			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => .AMethod()");
		}

		private void AMethod()
		{
			
		}

		[Test]
		public void MethodsCalls_WithNegation()
		{
			Expression<Func<string, bool>> expr = a => !a.ToUpperInvariant().Contains("TH");
			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => !(a.ToUpperInvariant().Contains(\"TH\"))");
		}

		[Test]
		public void WhenSystemStaticMethodCall_ShowTheClassName()
		{
			// the class-name is shorter than FullName (and, perhaps, is more clear and closer to the real usage)
			Expression<Func<string, bool>> expr = a => string.IsNullOrEmpty(a);
			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("a => String.IsNullOrEmpty(a)");

			expr = a => !string.IsNullOrEmpty(a);
			eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Contain("!").And.Contain("String.IsNullOrEmpty(a)");
		}

		[Test]
		public void NewArrayExpression()
		{
			Expression<Func<int[]>> expr = ()=> new[] {1,2,3};

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new[] {1, 2, 3}");
		}

		[Test]
		public void NewArrayExpressionWithStrings()
		{
			Expression<Func<string[]>> expr = () => new[] { "1", "2"};

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new[] {\"1\", \"2\"}");
		}

		[Test]
		public void NewArrayBoundsExpression()
		{
			Expression<Func<int[]>> expr = () => new int[5];

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new Int32[5]");
		}

		[Test]
		public void NewExpression()
		{
			Expression<Func<object>> expr = () => new object();

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new Object()");
		}

		private class MyClassWithNoDefCtor
		{
			public MyClassWithNoDefCtor(string aString, int aInt) {}
		}

		[Test]
		public void NewExpressionWithParameters_ClassWithoutDefCtor()
		{
			Expression<Func<object>> expr = () => new MyClassWithNoDefCtor("p1", 123);

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new MyClassWithNoDefCtor(\"p1\", 123)");
		}

		private class MyClassWithDefCtor
		{
			public string AString { get; set; }
			public int AInt { get; set; }
		}

		[Test]
		public void NewExpressionWithParameters_ClassWithDefCtor()
		{
			Expression<Func<object>> expr = () => new MyClassWithDefCtor { AString = "p1", AInt = 123};

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new MyClassWithDefCtor() {AString= \"p1\", AInt= 123}");
		}

		[Test]
		public void NewExpression_GenericType()
		{
			Expression<Func<object>> expr = () => new Dictionary<string, int>();

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new Dictionary<String, Int32>()");
		}

		[Test]
		public void ElementInitializer()
		{
			Expression<Func<object>> expr = () => new Dictionary<string, int> { { "k1", 1 }, { "k2", 2 } };

			var eb = new ExpressionStringBuilder(expr);
			eb.ToString().Should().Be.EqualTo("() => new Dictionary<String, Int32>() {{\"k1\", 1}, {\"k2\", 2}}");
		}
	}
}