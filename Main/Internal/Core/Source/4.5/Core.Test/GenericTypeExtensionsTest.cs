namespace EyeSoft.Core.Test
{
    using System;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class GenericTypeExtensionsTest
	{
		[TestMethod]
		public void SpecificGenericTypeIsSubclassOfGenericDefinition()
		{
			Action<string> genericAction = x => { };
			Action<string> specificAction = x => { };

			CompareGenericTypeWithSpecificType(genericAction, specificAction).Should().BeTrue();
		}

		private bool CompareGenericTypeWithSpecificType<T>(
			Action<T> genericAction,
			Action<string> specificAction)
		{
			var type = genericAction.GetType();
			var typeToCheck = specificAction.GetType();

			return type.EqualsOrSubclassOf(typeToCheck);
		}
	}
}