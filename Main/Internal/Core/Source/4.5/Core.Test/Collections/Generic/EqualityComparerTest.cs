namespace EyeSoft.Core.Test.Collections.Generic
{
    using System;
    using EyeSoft.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class EqualityComparerTest
	{
		[TestMethod]
		public void CompareTwoNullValues()
		{
			EqualityComparer.AreEquals(null, null).Should().BeTrue();
		}

		[TestMethod]
		public void CompareTwoEqualValueObjects()
		{
			EqualityComparer.AreEquals(1, 1).Should().BeTrue();
		}

		[TestMethod]
		public void CompareValueObjectsWithNull()
		{
			EqualityComparer.AreEquals(1, null).Should().BeFalse();
		}

		[TestMethod]
		public void CompareValueObjectsWithNullParametersOrderChanged()
		{
			EqualityComparer.AreEquals(null, 1).Should().BeFalse();
		}

		[TestMethod]
		public void CompareTwoDifferentValueObjects()
		{
			EqualityComparer.AreEquals(1, 2).Should().BeFalse();
		}

		[TestMethod]
		public void CompareTwoDifferentTypeOfValueObjectsExpectedException()
        {
            Action action = () => EqualityComparer.AreEquals(1, "2");
            
            action.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void CompareTwoEqualReferenceObjects()
		{
			var reference = new Reference();
			EqualityComparer.AreEquals(reference, reference).Should().BeTrue();
		}

		[TestMethod]
		public void CompareTwoDifferentValueObjects2()
		{
			const string Name = "1";

			EqualityComparer.AreEquals(new ReferenceEquatable(Name), new ReferenceEquatable(Name))
				.Should().BeTrue();
		}

		private class Reference
		{
		}

		private class ReferenceEquatable
		{
			private readonly string name;

			public ReferenceEquatable(string name)
			{
				this.name = name;
			}

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}

				var other = (ReferenceEquatable)obj;
				return name.Equals(other.name);
			}

			public override int GetHashCode()
			{
				return name.GetHashCode();
			}
		}
	}
}