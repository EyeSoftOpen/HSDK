namespace EyeSoft.Core.Collections.Generic
{
    using System.Collections;
    using System.Collections.Generic;

    public static class GenericComparerExtensions
	{
		public static IComparer ToNonGeneric<T>(this IComparer<T> comparer)
		{
			return ComparerFactory<T>.Create(comparer);
		}
	}
}