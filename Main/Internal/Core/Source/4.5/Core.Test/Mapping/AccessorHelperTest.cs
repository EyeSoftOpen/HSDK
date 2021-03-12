namespace EyeSoft.Core.Test.Mapping
{
    using System.Reflection;
    using EyeSoft.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class AccessorHelperTest
	{
		private const BindingFlags InstanceBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		[TestMethod]
		public void CheckFieldAccessor()
		{
			AccessorHelper
				.Get(typeof(FieldAccessor).GetField("test", InstanceBindingFlags))
				.Should().Be(Accessors.Field);
		}

		[TestMethod]
		public void CheckAutomaticPropertyAccessor()
		{
			AccessorHelper
				.Get(typeof(AutomaticPropertyAccessor).GetProperty("Test", InstanceBindingFlags))
				.Should().Be(Accessors.Property);
		}

		[TestMethod]
		public void CheckPropertyNoSettAccessor()
		{
			AccessorHelper
				.Get(new MemberInfoMetadata(typeof(PropertyNoSettAccessor).GetProperty("Test", InstanceBindingFlags)))
				.Should().Be(Accessors.PropertyNoSetter);
		}

		[TestMethod]
		public void CheckPropertyAccessor()
		{
			AccessorHelper
				.Get(typeof(PropertyFieldAccessor).GetProperty("Test", InstanceBindingFlags))
				.Should().Be(Accessors.Property);
		}

		[TestMethod]
		public void CheckProtectedPropertyAccessor()
		{
			AccessorHelper
				.Get(typeof(ProtectedPropertyAccessor).GetProperty("Test", InstanceBindingFlags))
				.Should().Be(Accessors.Property);
		}

		private class FieldAccessor
		{
			private string test;

			public FieldAccessor(string test)
			{
				this.test = test;
			}
		}

		private class PropertyNoSettAccessor
		{
			private readonly string test;

			public PropertyNoSettAccessor(string test)
			{
				this.test = test;
			}

			public string Test => test;
        }

		private class AutomaticPropertyAccessor
		{
			public string Test { get; set; }
		}

		private class ProtectedPropertyAccessor
		{
			protected string Test { get; set; }
		}

		private class PropertyFieldAccessor
		{
			private string test;

			public string Test
			{
				get => test;

                set => test = value;
            }
		}
	}
}