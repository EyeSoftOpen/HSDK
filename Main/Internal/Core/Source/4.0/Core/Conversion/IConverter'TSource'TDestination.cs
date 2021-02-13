namespace EyeSoft
{
	public interface IConverter<in TSource, out TDestination>
	{
		TDestination Convert(TSource source);
	}
}