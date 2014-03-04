namespace EyeSoft.Mapping
{
	using System;

	public interface IMapper
	{
		object Map(object source, Type sourceType, Type destinationType);

		TDestination Map<TDestination>(object source);

		TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
	}
}