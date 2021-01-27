namespace EyeSoft.Core.Collections.Generic
{
    using System.Collections;

    public static class ComparerExtensions
	{
		public static IComparer Invert(this IComparer comparer)
		{
			return new FuncToComparer((x, y) => -comparer.Compare(x, y));
		}
	}
}