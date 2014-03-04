namespace EyeSoft.Data.Nhibernate
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.Nhibernate.Mapping;
	using EyeSoft.Data.Nhibernate.Transactional;
	using EyeSoft.Domain.Transactional;
	using EyeSoft.Domain.Transactional.Discover;

	using NHibernate.Cfg.MappingSchema;
	using NHibernate.Mapping.ByCode;

	using SchemaAction = EyeSoft.Data.Common.SchemaAction;

	public class NhibernateConfiguration
	{
		private readonly Type unitOfWorkType;

		private readonly IDictionary<Type, NhibernateSessionHelper> databaseProviderSessionHelperDictionary =
			new Dictionary<Type, NhibernateSessionHelper>();

		internal NhibernateConfiguration(Type unitOfWorkType)
		{
			this.unitOfWorkType = unitOfWorkType;

			var entities =
				DomainEntityDiscover
					.Entities(unitOfWorkType);

			Mapping =
				new ModelAnnotationsMapper()
					.Map(entities);
		}

		public HbmMapping Mapping { get; private set; }

		public string XmlMapping
		{
			get { return Mapping.AsString(); }
		}

		internal UnitOfWork UnitOfWork(IDatabaseProvider databaseProvider, SchemaAction schemaAction)
		{
			return
				Initialize(databaseProvider, Mapping, schemaAction)
					.CreateUnitOfWork();
		}

		private NhibernateSessionHelper Initialize(
			IDatabaseProvider databaseProvider,
			HbmMapping mapping,
			SchemaAction schemaAction)
		{
			var databaseProviderType = databaseProvider.GetType();

			if (databaseProviderSessionHelperDictionary.ContainsKey(databaseProviderType))
			{
				return databaseProviderSessionHelperDictionary[databaseProviderType];
			}

			var nhibernateSessionHelper =
				new NhibernateSessionHelper(mapping, databaseProvider, unitOfWorkType, schemaAction);

			databaseProviderSessionHelperDictionary
				.Add(databaseProviderType, nhibernateSessionHelper);

			return nhibernateSessionHelper;
		}

		private class NhibernateSessionHelper
		{
			private readonly NhibernateHelper helper;

			private readonly Type unitOfWorkType;

			public NhibernateSessionHelper(
				HbmMapping mapping,
				IDatabaseProvider databaseProvider,
				Type unitOfWorkType,
				SchemaAction schemaAction)
			{
				helper = new NhibernateHelper(
					databaseProvider,
					mapping,
					new DefaultConfiguration(schemaAction));

				this.unitOfWorkType = unitOfWorkType;
			}

			public UnitOfWork CreateUnitOfWork()
			{
				Func<ITransactionalCollection, UnitOfWork> createUnitOfWork =
					transactionalCollection =>
						unitOfWorkType.CreateInstance<UnitOfWork>(transactionalCollection);

				var collection =
					new NhibernateTransactionalCollection(helper.OpenSession());

				return
					createUnitOfWork(collection);
			}
		}
	}
}