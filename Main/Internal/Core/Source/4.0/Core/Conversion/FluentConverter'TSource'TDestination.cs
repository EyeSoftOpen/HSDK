namespace EyeSoft
{
	public class FluentConverter<TSource, TDestination>
	{
		public IConverter<TSource, TDestination> Converter<TConverter>()
			where TConverter : IConverter<TSource, TDestination>, new()
		{
			var converter = new TConverter();

			Converters
				.Register(converter);

			return converter;
		}
	}
}