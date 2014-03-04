namespace EyeSoft.AutoMapper
{
	using System;

	using global::AutoMapper;

	using EyeSoft.Mapping;

	using Mapping = global::AutoMapper.Mapper;

	public class AutoMapperMapper : IMapper
	{
		public object Map(object source, Type sourceType, Type destinationType)
		{
			return Mapping.Map(source, sourceType, destinationType);
		}

		public TDestination Map<TDestination>(object source)
		{
			return Mapping.Map<TDestination>(source, m => m.CreateMissingTypeMaps = true);
		}

		public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
		{
			return Mapping.Map(source, destination);
		}

		public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
		{
			return Mapping.CreateMap<TSource, TDestination>();
		}
	}
}