using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SharpTestsEx.Tests
{
	
	public class EnumerableConstraintsTest
	{
		[Test]
		public void ShouldWork()
		{
			var ints = new[] { 1, 2, 3 };
			ints.Should().Have.SameSequenceAs(new[] { 1, 2, 3 });
			ints.Should().Not.Have.SameSequenceAs(new[] { 3, 2, 1 });
			ints.Should().Not.Be.Null();
			ints.Should().Not.Be.Empty();
			(new int[0]).Should().Be.Empty();
			((IEnumerable<int>)null).Should().Be.Null();
		}

		[Test]
		public void WhenNullThenEmptyShouldFail()
		{
			((IEnumerable<int>)null).Executing(a=> a.Should().Be.Empty()).Throws<AssertException>();			
		}

		[Test]
		public void SameSequenceAsWithParams()
		{
			var ints = new[] { 1, 2, 3 };
			ints.Should().Have.SameSequenceAs(1, 2, 3);
		}

		[Test]
		public void ShouldWorkUsingCustomMessage()
		{
			var title = "A title";
			try
			{
				(new int[0]).Should(title).Be.Null();
			}
			catch (AssertException ae)
			{
				ae.Message.Should().Contain(title);
			}
		}

		[Test]
		public void SameAsShouldWork()
		{
			var ints = new int[0];
			ints.Should().Be.SameInstanceAs(ints);
			ints.Should().Not.Be.SameInstanceAs(new int[0]);
		}

		[Test]
		public void ContainsShouldWork()
		{
			var ints = new[] { 1, 2, 3 };
			ints.Should().Contain(2);
			ints.Should().Not.Contain(4);
		}

		[Test]
		public void EquivalentsShouldWork()
		{
			var ints = new[] { 1, 2, 3 };
			ints.Should().Have.SameValuesAs((IEnumerable<int>)new[] { 3, 2, 1 });
			ints.Should().Have.SameValuesAs(3, 2, 1);
			Executing.This(() => ints.Should().Have.SameValuesAs((IEnumerable<int>) new[] {3, 2, 1, 4})).Should().Throw<AssertException>();
			ints.Should().Not.Have.SameValuesAs(new[] { 4, 2, 1 });
		}

		[Test]
		public void UniqueShouldWork()
		{
			(new[] { 1, 2, 3 }).Should().Have.UniqueValues();
			(new[] { 1, 2, 1 }).Should().Not.Have.UniqueValues();
		}

		[Test]
		public void SubsetOfShouldWork()
		{
			(new[] { 1, 2, 3 }).Should().Be.SubsetOf((IEnumerable<int>)new[] { 1, 2, 3, 4 });
			(new[] { 1, 2, 3 }).Should().Be.SubsetOf(1, 2, 3, 4);
			(new[] { 1, 2, 3 }).Should().Not.Be.SubsetOf(new[] { 1, 4, 5, 6 });
		}

		[Test]
		public void OrderedShouldWork()
		{
			(new[] { 1, 2, 3 }).Should().Be.Ordered();
			(new[] { 3, 2, 1 }).Should().Be.Ordered();
			(new[] { 4, 2, 5 }).Should().Not.Be.Ordered();
		}

		[Test]
		public void AndChainShouldWork()
		{
			var ints = new[] { 1, 2, 3 };
			ints.Should()
				.Have.SameSequenceAs(new[] { 1, 2, 3 })
				.And
				.Not.Have.SameSequenceAs(new[] { 3, 2, 1 })
				.And
				.Not.Be.Null()
				.And
				.Not.Be.Empty();
		}

		[Test]
		public void AndChainExtensionsShouldWork()
		{
			var ints = new[] { 1, 2, 3 };
			ints.Should().Contain(2).And.Not.Contain(4);
		}

		[Test]
		public void Count()
		{
			var ints = new[] { 1, 2, 3 };
			ints.Should().Have.Count.EqualTo(3);
			ints.Should().Not.Have.Count.EqualTo(2);
			ints.Should("chain not negated and negated").Contain(3).And.Not.Have.Count.LessThan(2);
		}

		[Test]
		public void ShouldUseEnumerableMessageBuilder()
		{
			var ints = new[] {1, 2, 3};
			Executing.This(() => ints.Should().Be.Null()).Should().Throw().And.ValueOf.Message.Should().Contain("[1, 2, 3]").And.Not.Contain("(null)");
			Executing.This(() => ints.Should().Be.Empty()).Should().Throw().And.ValueOf.Message.Should().Contain("[1, 2, 3]").And.Not.Contain("<Empty>");
			Executing.This(() => ints.Should().Have.SameSequenceAs(new[] { 3, 2, 1 })).Should().Throw().And.ValueOf.Message.Should().Contain("[1, 2, 3]").
				And.Contain("[3, 2, 1]");
			Executing.This(() => ints.Should().Have.SameValuesAs(4, 5, 6)).Should().Throw().And.ValueOf.Message.Should().Contain("[1, 2, 3]").And.Contain
				("[4, 5, 6]");
			Executing.This(() => ints.Should().Contain(4)).Should().Throw().And.ValueOf.Message.Should().Contain("[1, 2, 3]").
				And.Contain("4");
		}

		[Test]
		public void SameSequenceAsShouldUseSameSequenceAsMessageBuilder()
		{
			Executing.This(() => new[] { 1, 2, 3 }.Should().Have.SameSequenceAs(new[] { 3, 2, 1 })).Should().Throw().And.ValueOf.Message.Should().Contain(
				"Values differ");
			Executing.This(() => new[] { 1, 2, 3 }.Should().Not.Have.SameSequenceAs(new[] { 1, 2, 3 })).Should().Throw().And.ValueOf.Message.Should().Not.
				Contain("Values differ");
		}

		[Test]
		public void SameValuesAsShouldShowDifferences()
		{
			var e = Executing.This(() => new[] { 1, 2, 3 }.Should().Have.SameValuesAs((IEnumerable<int>)new[] { 3, 2, 4 })).Should().Throw().Exception;
			var messages = e.Message.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			messages.First(m => m.StartsWith("Differences :")).Should().Contain("1").And.Contain("4");

			messages = Executing.This(() => new[] { 1, 2, 3 }.Should().Have.SameValuesAs(1, 5)).Should().Throw().Exception
				.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			messages.First(m => m.StartsWith("Differences :")).Should().Contain("3").And.Contain("2").And.Contain("5");
		}

		[Test]
		public void SubsetOfShouldShowDifferences()
		{
			Exception e = Executing.This(() => new[] { 1, 2 }.Should().Be.SubsetOf((IEnumerable<int>)new[] { 3, 2, 4 })).Should().Throw().Exception;
			string[] messages = e.Message.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			messages.First(m => m.StartsWith("Differences :")).Should().Contain("1").And.Not.Contain("2").And.Not.Contain("4");

			messages =
				Executing.This(() => new[] { 1, 2 }.Should().Be.SubsetOf(1, 5)).Should().Throw().Exception.Message.Split(new[] { Environment.NewLine },
				                                                                                 StringSplitOptions.None);
			messages.First(m => m.StartsWith("Differences :")).Should().Contain("2").And.Not.Contain("1").And.Not.Contain("5");
		}

		[Test]
		public void UniqueShouldShowDifferences()
		{
			Executing.This(() => new[] { 1, 2, 1 }.Should().Have.UniqueValues()).Should().Throw().And.ValueOf
				.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
				.Should().Contain("Differences :[1]");
			Executing.This(() => new[] { 1, 2, 2, 3, 3 }.Should().Have.UniqueValues()).Should().Throw().And.ValueOf
				.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
				.Should().Contain("Differences :[2, 3]");
		}

		[Test]
		public void OrderedAscendingShouldWork()
		{
			(new[] { 1, 2, 3 }).Should().Be.OrderedAscending();
			(new[] { 3, 2, 1 }).Should().Not.Be.OrderedAscending();
			(new[] { 4, 2, 5 }).Should().Not.Be.OrderedAscending();
		}

		[Test]
		public void OrderedDescendingShouldWork()
		{
			(new[] { 3, 2, 1 }).Should().Be.OrderedDescending();
			(new[] { 1, 2, 3 }).Should().Not.Be.OrderedDescending();
			(new[] { 4, 2, 5 }).Should().Not.Be.OrderedDescending();
		}

		[Test]
		public void OrderedShouldUseEnumerableMessageBuilder()
		{
			var ints = new[] { 4, 2, 6 };
			Executing.This(() => ints.Should().Be.Ordered()).Should().Throw().And.ValueOf.Message.Should().Contain("[4, 2, 6]").And.Not.Contain("(null)");
		}

		[Test]
		public void CountSupportNull()
		{
			int[] ints = null;
			var e = Executing.This(() => ints.Should().Have.Count.EqualTo(3)).Should().Throw<AssertException>().Exception;
			e.Message.ToLowerInvariant().Should().Contain("not be null");
			Executing.This(() => ints.Should().Not.Have.Count.EqualTo(3)).Should().Throw<AssertException>().And.ValueOf.Message.
				ToLowerInvariant().Should().Contain("not be null");
		}

		[Test]
		public void OrderedByWithKeySelector()
		{
			var addresses = new[] { new Address { Street = "A", Number = 2 }, new Address { Street = "B", Number = 1 } };
			var orderedByNumber = from a in addresses orderby a.Number select a;
			var orderedByStreet = from a in addresses orderby a.Street select a;
			orderedByNumber.Should().Be.OrderedBy(a => a.Number);
			orderedByStreet.Should().Be.OrderedBy(a => a.Street);
			orderedByStreet.Should().Not.Be.OrderedBy(a => a.Number);
		}

		[Test]
		public void OrderedByWithKeySelectorMessage()
		{
			var addresses = new[] { new Address { Street = "A", Number = 2 }, new Address { Street = "B", Number = 1 } };
			var orderedByStreet = from a in addresses orderby a.Street select a;
			Executing.This(() => orderedByStreet.Should().Be.OrderedBy(a => a.Number)).Should().Throw().And.ValueOf
				.Message.Should().Contain("Values differ").And.Contain("a.Number");
		}

		class SingleUseEnumerable<T> : IEnumerable<T>
		{
			private readonly IEnumerable<T> _component;
			private bool _used;

			public SingleUseEnumerable(IEnumerable<T> component)
			{
				_component = component;
			}

			public IEnumerator<T> GetEnumerator()
			{
				if (_used)
					throw new InvalidOperationException("Enumerator can only be retrieved once.");
				_used = true;
				return _component.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		[Test]
		public void WhenUseSameValuesAsThenEnumerateOnece()
		{
			var sue = new SingleUseEnumerable<int>(new int[] { });
			sue.Should().Have.SameValuesAs(new int[] { });
		}

	}

	public class Address
	{
		public int Number { get; set; }
		public string Street { get; set; }
		public override string ToString()
		{
			return string.Format("{0}-{1}", Street, Number);
		}
	}
}