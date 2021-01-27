namespace SharpTestsEx.Test
{
    using System;
    using NUnit.Framework;

    public class ObjectExtensionsTest
	{
		public class A
		{
			private int oneField;

			public int OneField
			{
				get => oneField;
                set => oneField = value;
            }
		}

		[Test]
		public void NotAllowNull()
		{
			Executing.This(() => ((string) null).FieldValue<int>("something")).Should().Throw<ArgumentNullException>().And.
				ValueOf.ParamName.Should().Be.EqualTo("source");
		}

		[Test]
		public void NotExsistentField()
		{
			Executing.This(() => new A().FieldValue<int>("something")).Should().Throw<ArgumentOutOfRangeException>().And.ValueOf
				.ParamName.Should().Be.EqualTo("fieldName");
		}

		[Test]
		public void InvalidCast()
		{
			Executing.This(() => new A().FieldValue<string>("oneField")).Should().Throw<InvalidCastException>().And.ValueOf
				.Message.Should().Contain("oneField").And.Contain("System.Int32").And.Contain("System.String");
		}

		[Test]
		public void Work()
		{
			var a = new A();
			a.FieldValue<int>("oneField").Should().Be.EqualTo(0);
			a.OneField = 5;
			a.FieldValue<int>("oneField").Should().Be.EqualTo(5);
		}
	}
}