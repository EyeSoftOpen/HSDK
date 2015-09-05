namespace Model
{
	using System;

	public static class Resolver
	{
		private static IResolver localResolver;

		public static T Resolve<T>(Type type)
		{
			return localResolver.Resolve<T>(type);
		}

		internal static T Resolve<T>()
		{
			return localResolver.Resolve<T>();
		}

		public static void Set(IResolver resolver)
		{
			localResolver = resolver;
		}
	}
}