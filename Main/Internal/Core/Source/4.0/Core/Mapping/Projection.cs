namespace EyeSoft.Mapping
{
    using System;
    using System.Linq;

    public static class Projection
	{
		private static readonly Singleton<IProjection> singletonInstance = new Singleton<IProjection>();

		public static IQueryable<TResult> Project<TResult>(this IQueryable source)
		{
			return singletonInstance.Instance.Project<TResult>(source);
		}

		public static IQueryable<TResult> Project<TSource, TResult>(this IQueryable<TSource> source)
		{
			return singletonInstance.Instance.Project<TSource, TResult>(source);
		}

		public static void Set(Func<IProjection> projection)
		{
			singletonInstance.Set(projection);
		}
	}
}