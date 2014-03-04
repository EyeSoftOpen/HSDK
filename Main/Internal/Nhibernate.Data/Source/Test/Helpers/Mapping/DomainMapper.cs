namespace EyeSoft.Data.Nhibernate.Test.Helpers.Mapping
{
	using System;

	using NHibernate.Cfg.MappingSchema;
	using NHibernate.Mapping.ByCode;

	internal class DomainMapper
	{
		public HbmMapping Create(Type mappingType)
		{
			var mapper = new ModelMapper();

			var exportedTypes = mappingType.Assembly.GetExportedTypes();

			mapper.AddMappings(exportedTypes);

			mapper.AfterMapClass += (inspector, member, map) =>
				{
					map.Id(id => id.Generator(Generators.Assigned));
					map.Cache(
						c =>
							{
								c.Usage(CacheUsage.NonstrictReadWrite);
								c.Region("MyRegion");
							});
				};

		const string KeySuffix = "Id";

			mapper.AfterMapManyToOne += (inspector, member, map) =>
				map.Column(member.LocalMember.Name + KeySuffix);

			mapper.AfterMapBag += (inspector, member, map) =>
				map.Key(k => k.Column(member.LocalMember.DeclaringType.Name + KeySuffix));

			mapper.AfterMapJoinedSubclass += (inspector, member, map) =>
				map.Key(k => k.Column(member.BaseType.Name + KeySuffix));

			mapper.AfterMapBag += (inspetor, member, map) =>
				map.Cascade(Cascade.Merge);

			var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

			return mapping;
		}
	}
}