namespace EyeSoft.Mapping
{
	using System.Linq;

	public interface IProjection
	{
		IQueryable<TResult> Project<TResult>(IQueryable source);

		IQueryable<TResult> Project<TSource, TResult>(IQueryable<TSource> source);
	}
}