namespace EyeSoft.Data.Nhibernate.Mapping
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
    using Core.Collections.Generic;
    using Core.Conventions;
    using Core.Mapping;
    using Core.Mapping.Conventions;
    using Core.Mapping.Data;
    using Core.Reflection;
    using NHibernate.Cfg.MappingSchema;
	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class ModelAnnotationsMapper : IModelAnnotationsMapper
	{
		private readonly TypeMapper typeMapper;

		private readonly ModelMapper modelMapper =
			new ModelMapper();

		private readonly IDictionary<MappingStrategy, Type> strategyTypeDictionary =
			new Dictionary<MappingStrategy, Type>
				{
					{ MappingStrategy.TablePerType, typeof(ClassMapping<>) },
					{ MappingStrategy.TablePerTypeHierarchy, typeof(JoinedSubclassMapping<>) },
					{ MappingStrategy.TablePerConcreteType, typeof(UnionSubclassMapping<>) }
				};

		private readonly IDictionary<MappedType, MappingStrategy> entityList =
			new Dictionary<MappedType, MappingStrategy>();

		public ModelAnnotationsMapper(TypeMapperConventions conventions)
		{
			typeMapper =
				TypeMapperFactory.Create(conventions);
		}

		public ModelAnnotationsMapper(
			TypeConvention<object, object> typeConvention,
			IKeyConvention keyConvention,
			IVersionConvention versionConvention)
		{
			var conventions =
				new TypeMapperConventions(null, null, null, typeConvention, keyConvention, versionConvention);

			typeMapper =
				TypeMapperFactory.Create(conventions);
		}

		public IModelAnnotationsMapper Map<T>(MappingStrategy strategy = MappingStrategy.TablePerType)
			where T : class
		{
			return Map(typeMapper.Map(typeof(T)), strategy);
		}

		public HbmMapping Map(IEnumerable<Type> types)
		{
			types
				.ForEach(type => Map(typeMapper.Map(type)));

			return ((IModelAnnotationsMapper)this).CompileMapping();
		}

		public HbmMapping Map(IEnumerable<MappedType> mappedTypes)
		{
			mappedTypes
				.ForEach(x => Map(x));

			return ((IModelAnnotationsMapper)this).CompileMapping();
		}

		public IModelAnnotationsMapper Map(MappedType type, MappingStrategy strategy = MappingStrategy.TablePerType)
		{
			entityList.Add(type, strategy);
			return this;
		}

		HbmMapping IModelAnnotationsMapper.CompileMapping()
		{
			entityList
				.ForEach(AddMapping);

			var mapping =
				modelMapper
					.CompileMappingFor(entityList.Keys.Select(k => k.Source));

			mapping.defaultaccess = "backfield";

			return mapping;
		}

		private void AddMapping(KeyValuePair<MappedType, MappingStrategy> obj)
		{
			var holdersProvider = this.Invoke("GetConformistHoldersProvider", new[] { obj.Key.Source }, obj);

			holdersProvider =
				new TypeAnnotationsMapper()
					.Invoke("Map", new[] { obj.Key.Source }, new[] { obj.Key, holdersProvider });

			modelMapper.AddMapping((IConformistHoldersProvider)holdersProvider);
		}

		private IClassMapper<T> GetConformistHoldersProvider<T>(KeyValuePair<MappedType, MappingStrategy> type)
			where T : class
		{
			var annotationType = strategyTypeDictionary[type.Value];

			if (annotationType.IsGenericType)
			{
				annotationType = annotationType.MakeGenericType(type.Key.Source);
			}

			return (IClassMapper<T>)Activator.CreateInstance(annotationType);
		}
	}
}