namespace EyeSoft.Core
{
    using System.Collections.Generic;

    public static class Converters
	{
		private static readonly IDictionary<SourceDestination, object> converterDictionary =
			new Dictionary<SourceDestination, object>(new SourceDestinationComparer());

		public static FluentConverter<TSource, TDestination> Types<TSource, TDestination>()
		{
			return new FluentConverter<TSource, TDestination>();
		}

		public static FromRegister<TSource> From<TSource>()
		{
			return new FromRegister<TSource>();
		}

		internal static void Register<TSource, TDestination>(IConverter<TSource, TDestination> converter)
		{
			var sourceDestination = new SourceDestination<TSource, TDestination>();

			if (converterDictionary.ContainsKey(sourceDestination))
			{
				return;
			}

			converterDictionary
				.Add(sourceDestination, converter);
		}

		internal static IConverter<TSource, TDestination> GetConverter<TSource, TDestination>()
		{
			var sourceDestination = new SourceDestination<TSource, TDestination>();

			if (!converterDictionary.ContainsKey(sourceDestination))
			{
				const string Message =
					"The convertert for these types was not found. Register it using the Types<TSource, TDestination> method.";

				throw new KeyNotFoundException(Message);
			}

			return
				(IConverter<TSource, TDestination>)
				converterDictionary[sourceDestination];
		}
	}
}