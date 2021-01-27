namespace SharpTestsEx.Test
{
    using System.Text.RegularExpressions;
    using NUnit.Framework;

    public class StringConstraintsTest
	{
		[Test]
		public void ShouldWork()
		{
			"a".Should().Be.EqualTo("a");
			"a".Should().Not.Be.Empty();
			"a".Should().Not.Be.Null();
			"".Should().Be.Empty();
			"".Should().Not.Be.EqualTo("a");
			((string)null).Should().Be.Null();

			"a".Should().Not.Be.Empty()
				.And.Not.Be.Null();
		}

		[Test]
		public void ShouldWorkUsingCustomMessage()
		{
			var title = "My message";
			var e = Executing.This(() => "a".Should(title).Be.EqualTo("b")).Should().Throw<AssertException>().Exception;
			StringAssert.Contains(title, e.Message);
			e = Executing.This(() => "a".Should(title).Not.Be.EqualTo("a")).Should().Throw<AssertException>().Exception;
			StringAssert.Contains(title, e.Message);
		}

		[Test]
		public void ContainShouldWork()
		{
			const string something = "something";
			something.Should().Contain("some");
			something.ToUpperInvariant().Should().Contain("some".ToUpperInvariant());
			something.Should().Not.Contain("also");
			something.ToUpperInvariant().Should().Not.Contain("some");
		}

		[Test]
		public void StartsWithShouldWork()
		{
			const string something = "something";
			something.Should().StartWith("some");
			something.ToUpperInvariant().Should().StartWith("some".ToUpperInvariant());
			something.Should().Not.StartWith("ome");
		}

		[Test]
		public void EndsWithShouldWork()
		{
			const string something = "something";
			something.Should().EndWith("ing");
			something.ToUpperInvariant().Should().EndWith("ing".ToUpperInvariant());
			something.Should().Not.EndWith("some");
		}

		[Test]
		public void MatchShouldWork()
		{
			const string something = "something";
			something.Should().Match("ome[tT]h");
			something.ToUpperInvariant().Should().Match("ome[tT]h".ToUpperInvariant());
			something.Should().Not.Match("omeTh");
		}

		[Test]
		public void AndChainShouldWork()
		{
			"a".Should().Be.EqualTo("a").And.Not.Be.Empty();
			"a".Should().Not.Be.Empty().And.Not.Be.Null();
			"a".Should().Not.Be.EqualTo("b").And.Not.Be.Empty();
		}

		[Test]
		public void AndChainExtensionsShouldWork()
		{
			const string something = "something";

			something.Should()
				.StartWith("so")
				.And
				.EndWith("ing")
				.And
				.Contain("meth");

			something.Should()
				.StartWith("so")
				.And
				.EndWith("ing")
				.And
				.Not.Contain("body");

			something.Should()
				.Not.StartWith("ing")
				.And
				.Not.EndWith("so")
				.And
				.Not.Contain("body");
		}


		[Test]
		public void RegEx()
		{
			const RegexOptions ro = RegexOptions.Singleline | RegexOptions.IgnoreCase;
			var re = new Regex("a.b", ro);
			"a\nb".Should().Match(re);
			Executing.This(() => "zzz".Should().Match(re)).Should().Throw<AssertException>().And.ValueOf
				.Message.Should().Contain("a.b").And.Contain(ro.ToString());
		}

		[Test]
		public void NullOrEmpty()
		{
			string something = null;
			something.Should().Be.NullOrEmpty();
			string.Empty.Should().Be.NullOrEmpty();
			something = "something";
			something.Should().Not.Be.NullOrEmpty();
		}

		[Test]
		public void NullContain()
		{
			string something = null;
			Executing.This(() => something.Should().Contain("some")).Should().Throw<AssertException>();
		}

		[Test]
		public void NullForAnyConstraint()
		{
			string something = null;
			Executing.This(() => something.Should().EndWith("some")).Should().Throw<AssertException>();
			Executing.This(() => something.Should().StartWith("some")).Should().Throw<AssertException>();
			Executing.This(() => something.Should().Match("some")).Should().Throw<AssertException>();
			Executing.This(() => something.Should().Match(new Regex("a.b"))).Should().Throw<AssertException>();
		}

		[Test]
		public void BeShortCut()
		{
			"a".Should().Be("a");
			Executing.This(() => "a".Should().Not.Be("a")).Should().Throw<AssertException>();
			"a".Should().Not.Be("b");
			Executing.This(() => "a".Should().Be("b")).Should().Throw<AssertException>();
		}
	}
}