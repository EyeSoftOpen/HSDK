using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EyeSoft.Windows.Model.ServiceProxy.Collection.Property
{
	public interface ICollectionLoaded<out TService, TCollectionType>
		where TService : IDisposable
	{
		ICollectionPropertyFilled<TCollectionType> Fill<T>(Func<TService, IEnumerable<T>> func);

		ICollectionSorted<TService, TCollectionType> Sort(Expression<Func<TCollectionType, IComparable>> func);

		ICollectionSorted<TService, TCollectionType> Sort(Func<TCollectionType, TCollectionType, int> comparerFunc);

		ICollectionSorted<TService, TCollectionType> Sort(IComparer<TCollectionType> comparer);
	}
}