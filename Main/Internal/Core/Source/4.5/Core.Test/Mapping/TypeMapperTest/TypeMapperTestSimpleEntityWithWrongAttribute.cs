namespace EyeSoft.Test.Mapping
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Mapping;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class TypeMapperTestSimpleEntityWithWrongAttribute
	{
		[TestMethod]
		public void MapNullablePropertyWithRequiredAttributeExectedException()
		{
			var mappedType = TypeMapperFactory.Create().Map<Person>();

			Executing
				.This(() => mappedType.Primitives.ForEach(x => { }))
				.Should().Throw<ArgumentException>();
		}

		private abstract class Person
		{
			public string Name { get; set; }

			[Required]
			public decimal? Age { get; set; }
		}
	}
}