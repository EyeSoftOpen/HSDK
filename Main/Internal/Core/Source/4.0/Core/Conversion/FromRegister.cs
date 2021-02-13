namespace EyeSoft
{
	public class FromRegister<TSource>
	{
		public TDestination To<TDestination>(TSource source)
		{
			var converter =
				Converters.GetConverter<TSource, TDestination>();

			return converter.Convert(source);
		}
	}
}