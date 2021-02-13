namespace EyeSoft.Core.Test.Mapping
{
    using System.Linq;
    using System.Reflection;
    using EyeSoft.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class MemberInfoValidatorTest
	{
		[TestMethod]
		public void ValidateFieldExpectedTrue()
		{
			CheckMember<Field>("name", true);
		}

		[TestMethod]
		public void ValidateNoSetterPropertyExpectedFalse()
		{
			CheckMember<NoSetterProperty>("Name", false);
		}

		[TestMethod]
		public void ValidateWritePropertyExpectedTrue()
		{
			CheckMember<WriteProperty>("Name", true);
		}

		[TestMethod]
		public void ValidateReadWritePropertyExpectedTrue()
		{
			CheckMember<ReadWriteProperty>("Name", true);
		}

		private static void CheckMember<T>(string memberName, bool expected)
		{
			const BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

			new MemberInfoValidator()
				.IsValidFieldOrProperty(typeof(T).GetMember(memberName, BindingFlags).Single())
				.Should().Be.EqualTo(expected);
		}

		private abstract class Field
		{
			private string name = string.Empty;
		}

		private abstract class NoSetterProperty
		{
			public string Name => null;
        }

		private abstract class WriteProperty
		{
			public string Name { get; private set; }
		}

		private abstract class ReadWriteProperty
		{
			public string Name { get; set; }
		}
	}
}