namespace EyeSoft.Domain.Test.Transactional
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Domain.Transactional;
	using EyeSoft.Domain.Transactional.Implementations;
	using EyeSoft.Testing.Domain;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ReadOnlyRepositoryTest
	{
		[TestMethod]
		public void ReadOnlyRepository()
		{
			const string Expected = "Bill";

			var data = new List<IAggregate> { new Customer { Name = Expected } };

			using (var transactionalCollection = new ReadOnlyTransactionalCollection(data))
			{
				using (var uow = new CustomerOnlyUnitOfWork(transactionalCollection))
				{
					uow.CustomerOnlyRepository.Single().Name.Should().Be.EqualTo(Expected);
				}
			}
		}

		private class CustomerOnlyUnitOfWork : ReadOnlyUnitOfWork
		{
			public CustomerOnlyUnitOfWork(IReadTransactionalCollection transactionalCollection) : base(transactionalCollection)
			{
			}

			public CustomerOnlyRepository CustomerOnlyRepository { get; private set; }
		}

		private class CustomerOnlyRepository : ReadOnlyRepository<Customer>
		{
			public CustomerOnlyRepository(IReadOnlyRepository<Customer> session) : base(session)
			{
			}
		}

		private class Customer : Aggregate
		{
			public string Name { get; set; }
		}
	}
}