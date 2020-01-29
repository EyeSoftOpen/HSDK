namespace EyeSoft.Mapping
{
	using System;

	public static class Mapper
	{
		private static readonly Singleton<IMapper> singletonInstance = new Singleton<IMapper>();

		public static void Set(Func<IMapper> mapper)
		{
			singletonInstance.Set(mapper);
		}

		public static object Map(object source, Type sourceType, Type destinationType)
		{
			return source == null ? null : singletonInstance.Instance.Map(source, sourceType, destinationType);
		}

		public static TDestination Map<TDestination>(object source)
		{
			return source == null ? default : singletonInstance.Instance.Map<TDestination>(source);
		}

		public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
		{
			return Comparer.Equals(source, default) ? destination : singletonInstance.Instance.Map(source, destination);
		}
    }
}