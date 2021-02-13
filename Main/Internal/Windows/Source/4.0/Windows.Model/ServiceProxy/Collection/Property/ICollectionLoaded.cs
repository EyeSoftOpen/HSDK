namespace EyeSoft.Windows.Model.Collection.Property
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public interface ICollectionLoaded<out TService, TCollectionType>
		where TService : IDisposable
	{
		ICollectionPropertyFilled<TCollectionType> Fill<T>(Func<TService, IEnumerable<T>> func);

		ICollectionSorted<TService, TCollectionType> Sort(Expression<Func<TCollectionType, IComparable>> func);

		ICollectionSorted<TService, TCollectionType> Sort(Func<TCollectionType, TCollectionType, int> comparerFunc);

		ICollectionSorted<TService, TCollectionType> Sort(IComparer<TCollectionType> comparer);
	}
}