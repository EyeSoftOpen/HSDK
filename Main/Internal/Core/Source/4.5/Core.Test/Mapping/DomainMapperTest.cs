namespace EyeSoft.Core.Test.Mapping
{
    using System;
    using System.Linq;
    using EyeSoft.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class DomainMapperTest
	{
		[TestMethod]
		public void VerifyAllMappedEntityAreReturned()
		{
			new DomainMapper()
				.Register<CustomerAggregate>()
				.Register<HeadOffice>()
				.Register<Order>()
				.MappedTypes()
				.Count().Should().Be(3);
		}

		[TestMethod]
		public void MapTheSameEntityMoreThanOnceExpectedException()
		{
            Action action = () =>
			    new DomainMapper()
				    .Register<CustomerAggregate>()
				    .Register<CustomerAggregate>();
            
            action.Should().Throw<ArgumentException>();
		}

		private class CustomerAggregate
		{
		}

		private class HeadOffice
		{
		}

		private class Order
		{
		}
	}
}