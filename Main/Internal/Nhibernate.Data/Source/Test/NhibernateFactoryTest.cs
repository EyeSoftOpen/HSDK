namespace EyeSoft.Data.Nhibernate.Test
{
	using System;

	using EyeSoft.Data.Nhibernate;
	using EyeSoft.Data.Nhibernate.Test.Helpers;
	using EyeSoft.Testing.Domain.Helpers.Domain4;
	using EyeSoft.Testing.Domain.Helpers.Domain4.Trasactional;
	using EyeSoft.Testing.Domain.Helpers.Domain5.Transactional;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	using SharpTestsEx;

	using SchemaAction = EyeSoft.Data.Common.SchemaAction;

	[TestClass]
	public class NhibernateFactoryTest
	{
		[TestMethod]
		public void CheckMapper()
		{
			typeof(ClassMapping<Post>).EqualsOrSubclassOf(typeof(IClassMapper<>)).Should().Be.True();
		}

		[TestMethod]
		public void AddAggregateAndCheck()
		{
			var dbProvider = NhibernateFactory.DatabaseProvider<BloggerUnitOfWork>();

			Func<BloggerUnitOfWork> factory =
				() => NhibernateFactory.Create<BloggerUnitOfWork>(SchemaAction.Create);

			BloggerPersistenceHelper.Check(factory, dbProvider);
		}

		[TestMethod]
		public void AddAggregateAndCheckUsingCustomDatabase()
		{
			var dbProvider = DataProviderHelper.Create(DbNames.Factory);

			Func<AdministrationUnitOfWork> factory =
				() => NhibernateFactory.CreateByDatabaseProvider<AdministrationUnitOfWork>(dbProvider, SchemaAction.Create);

			AdministrationPersistenceHelper.Check(factory, dbProvider);
		}
	}
}