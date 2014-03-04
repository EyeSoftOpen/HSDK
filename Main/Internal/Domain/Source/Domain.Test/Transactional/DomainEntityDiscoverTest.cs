namespace EyeSoft.Domain.Test.Transactional
{
	using System.Linq;

	using EyeSoft.Domain.Transactional.Discover;
	using EyeSoft.Testing.Domain.Helpers.Domain4;
	using EyeSoft.Testing.Domain.Helpers.Domain4.Trasactional;
	using EyeSoft.Testing.Domain.Helpers.Domain5;
	using EyeSoft.Testing.Domain.Helpers.Domain5.Transactional;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class DomainEntityDiscoverTest
	{
		[TestMethod]
		public void DiscoverEntitiesFromUnitOfWorkSample1()
		{
			UnitOfWorkEntities
				.Discover<BloggerUnitOfWork>()
				.Select(mappedType => mappedType.Source)
				.Should().Have.SameSequenceAs(typeof(Blog), typeof(Post));
		}

		[TestMethod]
		public void DiscoverEntitiesFromUnitOfWorkSample2()
		{
			UnitOfWorkEntities
				.Discover<AdministrationUnitOfWork>()
				.Select(mappedType => mappedType.Source)
				.Should().Have.SameSequenceAs(typeof(Invoice), typeof(InvoiceHead));
		}
	}
}