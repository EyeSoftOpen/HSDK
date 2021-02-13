namespace EyeSoft
{
	internal class SourceDestination<TSource, TDestination>
		: SourceDestination
	{
		public SourceDestination()
			: base(typeof(TSource), typeof(TDestination))
		{
		}
	}
}