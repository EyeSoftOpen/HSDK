﻿namespace EyeSoft.Core.Test.Mapping
{
    using System.Linq;
    using EyeSoft.Mapping;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ProjectTest
	{
		private const string ExpectedName = "Bill";
		private static readonly IQueryable customers = new[] { new Customer(ExpectedName, "Elm Street") }.AsQueryable();

		[TestMethod]
		public virtual void VerifyProjectWorks()
		{
			Projection.Set(Create);

			var mapped = customers.Project<CustomerProjection>();

			mapped.Single().Name.Should().Be(ExpectedName);
		}

		protected virtual IProjection Create()
		{
			return new TestMapperProjection();
		}

		protected class CustomerProjection
		{
			public string Name { get; set; }
		}

		private class TestMapperProjection : IProjection
		{
			public IQueryable<TResult> Project<TResult>(IQueryable source)
			{
				return
					source.Cast<Customer>().Select(x => new CustomerProjection { Name = x.Name }).Cast<TResult>();
			}

			public IQueryable<TResult> Project<TSource, TResult>(IQueryable<TSource> source)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
