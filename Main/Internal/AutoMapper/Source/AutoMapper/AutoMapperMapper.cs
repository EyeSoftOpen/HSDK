namespace EyeSoft.AutoMapper
{
	using System;

	using global::AutoMapper;
    using IMapper = Mapping.IMapper;

    public class AutoMapperMapper : IMapper
	{
        public object Map(object source, Type sourceType, Type destinationType)
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap(sourceType, destinationType));

			var mapper = config.CreateMapper();

			return mapper.Map(source, sourceType, destinationType);
		}

		public TDestination Map<TDestination>(object source)
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap(source.GetType(), typeof(TDestination)));

			var mapper = config.CreateMapper();

			return mapper.Map<TDestination>(source);
        }

		public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap(source.GetType(), typeof(TDestination)));

			var mapper = config.CreateMapper();

			return mapper.Map(source, destination);
		}
	}
}